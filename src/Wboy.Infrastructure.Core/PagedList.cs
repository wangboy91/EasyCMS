using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wboy.Infrastructure.Core
{
    public class PagedList<TEntity>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PagedList()
        {
            Rows = new List<TEntity>();
        }

        /// <summary>
        /// ctor with params
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数量</param>
        public PagedList(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allList">所有数据</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数量</param>
        public PagedList(IList<TEntity> allList, int pageIndex, int pageSize)
        {
            this.Records = allList.Count();
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Rows= allList.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curPageList">当页数据</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="totalCount">总记录数</param>
        public PagedList(IList<TEntity> curPageList, int pageIndex, int pageSize, int totalCount)
        {
            this.Rows = curPageList;
            this.Records = totalCount;
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Records { set; get; }
        /// <summary>
        /// 当前页的所有项
        /// </summary>
        public IList<TEntity> Rows { set; get; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { set; get; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { set; get; }
        /// <summary>
        /// 页总数
        /// </summary>
        public int Total
        {
            get
            {
               var  totalPages = Records / PageSize;
                if (Records % PageSize > 0)
                    totalPages++;
                return totalPages;
            }
        }
    }
}
