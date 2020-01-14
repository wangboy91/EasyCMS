using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Infrastructure.Core;

namespace Wboy.Domain.AdminModule.IRepository
{
    public interface IMenuRepository : IRepository<MenuEntity>
    {

        #region 获取数据
        /// <summary>
        /// 获取同级菜单的code
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        Task<IList<string>> GetCodesAsync(Guid? parentId);
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="ids">菜单ids</param>
        /// <returns></returns>
        Task<IList<MenuEntity>> GetMenusByIds(IEnumerable<Guid> ids);
        /// <summary>
        /// 异步获取菜单分页列表
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页值</param>
        /// <param name="keyword">关键字</param>
        /// <param name="excludeType">排除类型</param>
        /// <returns></returns>
        Task<PagedList<MenuEntity>> GetPageList(int pageIndex, int pageSize, string keyword, MenuType? excludeType, Guid? parnetId);
        /// <summary>
        /// 获取菜单(非按钮)
        /// </summary>
        /// <param name="ids">菜单ids</param>
        /// <returns></returns>
        Task<IList<MenuEntity>> GetMenusNotButton(IList<Guid> ids);
        Task<IList<MenuEntity>> GetAllMenus();
        #endregion
        #region 验证数据
        /// <summary>
        /// 验证是否存在
        /// </summary>
        /// <param name="menuIds">菜单ids</param>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        Task<bool> ExistByIdsAndUrl(IList<Guid> menuIds, string url);
        /// <summary>
        /// 通过父级id获取菜单
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        Task<IList<MenuEntity>> GetListByParentId(Guid parentId);
        #endregion
    }
}
