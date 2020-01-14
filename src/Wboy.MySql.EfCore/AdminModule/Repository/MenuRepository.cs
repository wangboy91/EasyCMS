using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain.AdminModule;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Domain.AdminModule.IRepository;
using Wboy.Infrastructure.Core;

namespace Wboy.MySql.EfCore.AdminModule.Repository
{
    public class MenuRepository : Repository<MenuEntity>, IMenuRepository
    {
        public MenuRepository(EfUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
        #region 获取数据
        /// <summary>
        /// 获取同级菜单的code
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        public async Task<IList<string>> GetCodesAsync(Guid? parentId)
        {
            return await _dbSet.Where(x => x.ParentId == parentId).Select(x => x.Code).ToListAsync();
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="ids">菜单ids</param>
        /// <returns></returns>
        public async Task<IList<MenuEntity>> GetMenusByIds(IEnumerable<Guid> ids)
        {
            return await _dbSet.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
        /// <summary>
        /// 异步获取菜单分页列表
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页值</param>
        /// <param name="keyword">关键字</param>
        /// <param name="excludeType">排除类型</param>
        /// <returns></returns>
        public async Task<PagedList<MenuEntity>> GetPageList(int pageIndex, int pageSize, string keyword, MenuType? excludeType, Guid? parentId)
        {
            var query = _dbSet.Where(x => true && x.ParentId == parentId);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }
            if (excludeType.HasValue)
            {
                query = query.Where(x => x.Type == excludeType.Value);
            }
            var s = await query.OrderBy(x => x.Order)
                               .PagingAsync(pageIndex, pageSize);
            return s;
        }
        /// <summary>
        /// 获取菜单(非按钮)
        /// </summary>
        /// <param name="ids">菜单ids</param>
        /// <returns></returns>
        public async Task<IList<MenuEntity>> GetMenusNotButton(IList<Guid> ids)
        {
            return await _dbSet.Where(x => x.Type != MenuType.Button && ids.Contains(x.Id)).ToListAsync();
        }
        /// <summary>
        /// 通过父级id获取菜单
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        public async Task<IList<MenuEntity>> GetListByParentId(Guid parentId)
        {
            return await _dbSet.Where(x => x.ParentId.HasValue && x.ParentId.Value == parentId).ToListAsync();
        }

        public async Task<IList<MenuEntity>> GetAllMenus()
        {
            return await _dbSet.Where(x => x.Type != MenuType.Button).ToListAsync();
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 验证是否存在
        /// </summary>
        /// <param name="menuIds">菜单ids</param>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public async Task<bool> ExistByIdsAndUrl(IList<Guid> menuIds, string url)
        {
            return await _dbSet.AnyAsync(x => menuIds.Contains(x.Id) && x.Url.StartsWith(url));
        }
        #endregion
    }
}
