using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Infrastructure.Core;

namespace Wboy.Domain.AdminModule.IRepository
{
    public interface IPageViewRepository : IRepository<PageViewEntity>
    {
        #region 获取数据
        /// <summary>
        /// 异步获取登陆日志分页列表
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页值</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        Task<PagedList<PageViewEntity>> GetPageList(int pageIndex, int pageSize, string keyword);
        #endregion
    }
}
