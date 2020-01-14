using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Domain.AdminModule.IRepository;
using Wboy.Infrastructure.Core;

namespace Wboy.MySql.EfCore.AdminModule.Repository
{
    public class RoleRepository : Repository<RoleEntity>, IRoleRepository
    {
        public RoleRepository(EfUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        private EfUnitOfWork CurrentUnitOfWork => UnitOfWork as EfUnitOfWork;
        DbSet<RoleMenuEntity> _roleMenuDbset;
        public DbSet<RoleMenuEntity> RoleMenuDbset => _roleMenuDbset ?? (_roleMenuDbset = CurrentUnitOfWork.Set<RoleMenuEntity>());

        #region 获取数据
        /// <summary>
        /// 获取角色的菜单ids
        /// </summary>
        /// <param name="roleIds">角色ids</param>
        /// <returns></returns>
        public async Task<IList<Guid>> GetMenuIdsByRoleIds(IList<Guid> roleIds)
        {
            return await RoleMenuDbset.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.MenuId).Distinct().ToListAsync();
        }
        /// <summary>
        /// 获取角色的菜单ids
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<IList<Guid>> GetMenuIdsByRoleId(Guid roleId)
        {
            return await RoleMenuDbset.Where(x => roleId == x.RoleId).Select(x => x.MenuId).ToListAsync();
        }
        /// <summary>
        /// 获取角色的菜单
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public async Task<IList<RoleMenuEntity>> GetRoleMenus(Guid roleId)
        {
            return await RoleMenuDbset.Where(x => roleId == x.RoleId).ToListAsync();
        }
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="ids">角色ids</param>
        /// <returns></returns>
        public async Task<IList<RoleEntity>> GetRolesByIds(IEnumerable<Guid> ids)
        {
            return await _dbSet.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
        /// <summary>
        /// 异步获取角色分页列表
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页值</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public async Task<PagedList<RoleEntity>> GetPageList(int pageIndex, int pageSize, string keyword)
        {
            var query = _dbSet.Where(x => true);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }
            return await query.OrderByDescending(x => x.AddTime)
                               .PagingAsync(pageIndex, pageSize);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 批量添加角色菜单
        /// </summary>
        /// <param name="entities"></param>
        public void InsertRoleMenus(IList<RoleMenuEntity> entities)
        {
            RoleMenuDbset.AddRange(entities);
        }
        /// <summary>
        /// 批量删除角色菜单
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRoleMenus(IList<RoleMenuEntity> entities)
        {
            RoleMenuDbset.RemoveRange(entities);
        }
        #endregion
    }
}
