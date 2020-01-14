using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Application.Service.AdminModule.IAppService;
using Wboy.Domain;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Domain.AdminModule.IRepository;
using Wboy.Infrastructure.Core;
using Wboy.Infrastructure.Core.Tree;

namespace Wboy.Application.Service.AdminModule.AppService
{
    public class RoleAppService : IRoleAppService
    {
        #region 基础属性
        private readonly IRoleRepository _roleRepository;
        private readonly IMenuRepository _menuRepository;
        public RoleAppService(IRoleRepository roleRepository,
            IMenuRepository menuRepository)
        {
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<RoleDto> FindAsync(Guid id)
        {
            var entity = await _roleRepository.GetAsync(id);
            return new RoleDto(entity);
        }

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        public async Task<PagedList<RoleDto>> SearchAsync(RoleFilters filters)
        {
            if (filters == null)
                return new PagedList<RoleDto>();
            var page = await _roleRepository.GetPageList(filters.page-1, filters.rows, filters.keywords);
            return new PagedList<RoleDto>()
            {
                Rows = page.Rows.Select(x => new RoleDto(x)).ToList(),
                PageIndex = page.PageIndex,
                PageSize = page.PageSize,
                Records = page.Records
            };
        }
        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <returns></returns>
        public async Task<List<TreeDto>> GetTreesAsync()
        {
            var list = await _roleRepository.GetAllAsync();
            return list.Select(x => new TreeDto()
            {
                id = x.Id,
                isParent = false,
                name = x.Name,
                open = false,
                pId = null
            }).ToList();
        }
        /// <summary>
        /// 获取角色的菜单树
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<string> AuthenRoleMenuTreeListAsync(Guid roleId)
        {
            var authenMenuIds = await _roleRepository.GetMenuIdsByRoleId(roleId);
            var menus = await _menuRepository.GetAllAsync();
            var treeList = menus.Select(x => new TreeModel()
            {
                id = x.Id.ToString(),
                text = x.Name,
                value = x.Id.ToString(),
                isexpand = true,
                complete = true,
                showcheck = true,
                checkstate = authenMenuIds.Count(t => t == x.Id),
                hasChildren = menus.Count(t => t.ParentId == x.Id) == 0 ? false : true,
                parentId = x.ParentId.ToString(),
                img = "fa fa-tv"
            }).ToList();
            return treeList.TreeToJson();
        }


        #endregion

        #region 提交数据
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto">角色模型</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(RoleDto dto)
        {
            var entity = new RoleEntity()
            {
                Id = Domain.NewId.NewSecuentialGuid(),
                Name = dto.Name,
                Description = dto.Description,
                AddTime = DateTime.Now
            };
            _roleRepository.Insert(entity);
            return await _roleRepository.UnitOfWork.CommitAsync() > 0 ? entity.Id : Guid.Empty;
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="dto">角色模型</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(RoleDto dto)
        {
            var entity = await _roleRepository.GetAsync(dto.Id);
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            return await _roleRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(IEnumerable<Guid> ids)
        {
            var entities = await _roleRepository.GetRolesByIds(ids);
            entities.ForEach(x => _roleRepository.Delete(x));
            return await _roleRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _roleRepository.GetAsync(id);
            if (entity == null)
                throw new NeedToShowFrontException("要删除的数据不存在！");
            _roleRepository.Delete(entity);
            return await _roleRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SetRoleMenusAsync(List<RoleMenuDto> datas)
        {
            if (!datas.AnyOne()) return false;

            var roleId = datas.First().RoleId;
            var olds = _roleRepository.Get(roleId)?.RoleMenus.ToList();
            var oldIds = olds.Select(x => x.MenuId);
            var newIds = datas.Select(x => x.MenuId);
            var adds = datas.Where(x => !oldIds.Contains(x.MenuId)).ToList();
            var removes = olds.Where(x => !newIds.Contains(x.MenuId)).ToList();
            if (adds.AnyOne())
            {
                var entities = adds.Select(x =>
                                        new RoleMenuEntity()
                                        {
                                            Id = Domain.NewId.NewSecuentialGuid(),
                                            MenuId = x.MenuId,
                                            RoleId = x.RoleId,
                                            AddTime = DateTime.Now
                                        })
                                    .ToList();
                _roleRepository.InsertRoleMenus(entities);
            }
            if (removes.AnyOne())
            {
                _roleRepository.DeleteRoleMenus(removes);
            }
            return await _roleRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 清空该角色下的所有权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<bool> ClearRoleMenusAsync(Guid roleId)
        {
            if (roleId == Guid.Empty) return false;

            var list = _roleRepository.Get(roleId)?.RoleMenus.ToList();
            if (list == null || list.Count == 0) return true;

            _roleRepository.DeleteRoleMenus(list);
            return await _roleRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="menuIds">菜单ids</param>
        /// <returns></returns>
        public async Task<bool> SetRoleMenusAsync(Guid roleId, IList<Guid> menuIds)
        {
            var olds = await _roleRepository.GetRoleMenus(roleId);
            var oldIds = olds.Select(x => x.MenuId);
            var adds = menuIds.Where(x => !oldIds.Contains(x)).ToList();
            var removes = olds.Where(x => !menuIds.Contains(x.MenuId)).ToList();
            if (adds.AnyOne())
            {
                var entities = adds.Select(x =>
                                        new RoleMenuEntity()
                                        {
                                            Id = Domain.NewId.NewSecuentialGuid(),
                                            MenuId = x,
                                            RoleId = roleId,
                                            AddTime = DateTime.Now
                                        })
                                    .ToList();
                _roleRepository.InsertRoleMenus(entities);
            }
            if (removes.AnyOne())
            {
                _roleRepository.DeleteRoleMenus(removes);
            }
            return await _roleRepository.UnitOfWork.CommitAsync() > 0;
        }
        #endregion
    }
}
