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
    public class PageViewRepository : Repository<PageViewEntity>, IPageViewRepository
    {
        public PageViewRepository(EfUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        #region 获取数据
        /// <summary>
        /// 异步获取登陆日志分页列表
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页值</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public async Task<PagedList<PageViewEntity>> GetPageList(int pageIndex, int pageSize, string keyword)
        {
            var query = _dbSet.Where(x => true);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.LoginName.Contains(keyword));
            }
            return await query.OrderByDescending(x => x.AddTime)
                               .PagingAsync(pageIndex, pageSize);
        }
        #endregion
    }
}
