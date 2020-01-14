using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wboy.Infrastructure.Core;


// ReSharper disable once CheckNamespace
namespace WebApp
{
    public class JqGridModel<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Records { set; get; }
        /// <summary>
        /// 当前页的所有项
        /// </summary>
        public IList<T> Rows { set; get; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { set; get; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int Pagesize { set; get; }

        /// <summary>
        /// 页总数
        /// </summary>
        public int Total { set; get; }
        public JqGridModel(PagedList<T> dtos)
        {
            Rows = dtos.Rows;
            Records = dtos.Records;
            Page = dtos.PageIndex+1;
            Pagesize = dtos.PageSize;
            Total = dtos.Total;
        }

        public JqGridModel()
        {
            
        }
    }
}
