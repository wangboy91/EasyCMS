using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Infrastructure.Core;

namespace Wboy.Domain.AdminModule.IRepository
{
    public interface IRoleRepository : IRepository<RoleEntity>
    {
        #region 获取数据
        /// <summary>
        /// 获取角色的菜单ids
        /// </summary>
        /// <param name="roleIds">角色ids</param>
        /// <returns></returns>
        Task<IList<Guid>> GetMenuIdsByRoleIds(IList<Guid> roleIds);
        /// <summary>
        /// 获取角色的菜单ids
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<IList<Guid>> GetMenuIdsByRoleId(Guid roleId);
        /// <summary>
        /// 获取角色的菜单
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<IList<RoleMenuEntity>> GetRoleMenus(Guid roleId);
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="ids">角色ids</param>
        /// <returns></returns>
        Task<IList<RoleEntity>> GetRolesByIds(IEnumerable<Guid> ids);
        /// <summary>
        /// 异步获取角色分页列表
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页值</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        Task<PagedList<RoleEntity>> GetPageList(int pageIndex, int pageSize, string keyword);
        #endregion

        #region 提交数据
        /// <summary>
        /// 批量添加角色菜单
        /// </summary>
        /// <param name="entities"></param>
        void InsertRoleMenus(IList<RoleMenuEntity> entities);
        /// <summary>
        /// 批量删除角色菜单
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRoleMenus(IList<RoleMenuEntity> entities);
        #endregion
    }
}
