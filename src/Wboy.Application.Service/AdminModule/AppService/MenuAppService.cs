using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Application.Dto.AdminModule.Dto;
using Wboy.Application.Dto.AdminModule.Filters;
using Wboy.Application.Service.AdminModule.IAppService;
using Wboy.Domain;
using Wboy.Domain.AdminModule;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Domain.AdminModule.IRepository;
using Wboy.Infrastructure.Core;
using Wboy.Infrastructure.Core.Tree;

namespace Wboy.Application.Service.AdminModule.AppService
{
    public class MenuAppService : IMenuAppService
    {
        #region 基础属性
        private readonly IMenuRepository _menuRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public MenuAppService(IMenuRepository menuRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _menuRepository = menuRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<MenuDto> FindAsync(Guid id)
        {
            var entity = await _menuRepository.GetAsync(id);
            var dto = new MenuDto(entity);
            if (dto.ParentId.HasValue)
            {
                var parent = await _menuRepository.GetAsync(entity.ParentId.Value);
                dto.ParentName = parent.Name;
            }
            return dto;
        }

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        public async Task<PagedList<MenuDto>> SearchAsync(MenuFilters filters)
        {
            if (filters == null)
                return new PagedList<MenuDto>();
            var page = await _menuRepository.GetPageList(filters.page - 1, filters.rows, filters.keywords, (MenuType?)filters?.ExcludeType, filters.ParentId);
            return new PagedList<MenuDto>()
            {
                Rows = page.Rows.Select(x => new MenuDto(x)).ToList(),
                PageIndex = page.PageIndex,
                PageSize = page.PageSize,
                Records = page.Records
            };
        }

        /// <summary>
        /// 获取用户拥有的权限菜单（不包含按钮）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetMyMenusAsync(Guid userId)
        {
            var roleIds = await _userRepository.GetUserRoleIdsAsync(userId);
            var menuIds = await _roleRepository.GetMenuIdsByRoleIds(roleIds);
            var list = await _menuRepository.GetMenusNotButton(menuIds);
            return list.Select(x => new MenuDto(x)).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<MenuDto>> GetAllMenus()
        {
            var list = await _menuRepository.GetAllMenus();
            return list.Select(x => new MenuDto(x)).ToList();
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<List<TreeDto>> GetTreesAsync()
        {
            var list = await _menuRepository.GetAllAsync();
            return list.OrderBy(x => x.Order)?.Select(x => new TreeDto()
            {
                id = x.Id,
                isParent = list.Any(p => p.ParentId.HasValue && p.ParentId.Value == x.Id),
                name = x.Name,
                open = false,
                pId = x.ParentId
            }).ToList();
        }

        /// <summary>
        /// 通过角色ID获取拥有的菜单权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetMenusByRoleIdAsync(Guid roleId)
        {
            var menuIds = _roleRepository.Get(roleId)?.RoleMenus.Select(x => x.MenuId).ToList();
            var list = await _menuRepository.GetMenusByIds(menuIds);
            return list.Select(x => new MenuDto(x)).ToList();
        }
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public string GetTreeListJson(string keyword)
        {
            var list = _menuRepository.GetAll().Where(x => x.Type!=MenuType.Button).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                list = list.TreeWhere(f => f.Name.Contains(keyword), "");
            }
            var treeList = list.OrderBy(x => x.Order).Select(x => new TreeModel()
            {
                id = x.Id.ToString(),
                text = x.Name,
                value = x.Id.ToString(),
                isexpand = true,
                complete = true,
                hasChildren = list.Count(t => t.ParentId == x.Id) == 0 ? false : true,
                parentId = x.ParentId.ToString(),
                img =  x.Icon
            }).ToList();
            return treeList.TreeToJson();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(MenuDto dto)
        {
            var entity = new MenuEntity()
            {
                Id = Domain.NewId.NewSecuentialGuid(),
                Name = dto.Name,
                ParentId = dto.ParentId,
                //Type = (MenuType)dto.Type,
                Order = dto.Order,
                Url = dto.Url,
                Icon = dto.Icon,
                AddTime = DateTime.Now
            };
            var pathCodes = GetPathCodes();
            var existsCode = await _menuRepository.GetCodesAsync(dto.ParentId);
            var pathCode = pathCodes.FirstOrDefault(item => !existsCode.Contains(item));
            entity.Code = pathCode.Trim();
            if (entity.ParentId.HasValue)
            {
                var parent = await _menuRepository.GetAsync(entity.ParentId.Value);
                entity.PathCode = string.Concat(parent.PathCode.Trim(), entity.Code.Trim());
                entity.Type = parent.Type == MenuType.Module ? MenuType.Menu : MenuType.Button;
            }
            else
            {
                entity.PathCode = entity.Code.Trim();
                entity.Type = MenuType.Module;
            }
            _menuRepository.Insert(entity);
            return await _menuRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 获取路径码
        /// </summary>
        /// <returns></returns>
        private IList<string> GetPathCodes()
        {
            //生成路径码
            var codes = new List<string>(26);
            for (var i = 65; i <= 90; i++)
            {
                codes.Add(((char)i).ToString());
            }
            return (from a in codes
                    from b in codes
                    select a + b).OrderBy(item => item).ToList();
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(MenuDto dto)
        {
            var entity = await _menuRepository.GetAsync(dto.Id);
            if (entity == null)
                throw new NeedToShowFrontException("菜单不存在");
            entity.Name = dto.Name;
            entity.Url = dto.Url;
            entity.Order = dto.Order;
            entity.ParentId = dto.ParentId;
            entity.Icon = dto.Icon;
            return await _menuRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(IEnumerable<Guid> ids)
        {
            var entities = await _menuRepository.GetMenusByIds(ids);
            foreach (var entity in entities)
            {
                _menuRepository.Delete(entity);
            }
            return await _menuRepository.UnitOfWork.CommitAsync() > 0;
        }
        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _menuRepository.GetAsync(id);
            var childrens = await _menuRepository.GetListByParentId(id);
            if (entity == null)
                throw new NeedToShowFrontException("要删除的数据不存在！");
            if (childrens.AnyOne())
            {
                _menuRepository.Delete(childrens);
            }
            _menuRepository.Delete(entity);
            return await _menuRepository.UnitOfWork.CommitAsync() > 0;
        }

        #endregion
    }
}
