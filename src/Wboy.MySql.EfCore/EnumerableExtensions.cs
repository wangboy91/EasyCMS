using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Domain;
using Wboy.Infrastructure.Core;

namespace Wboy.MySql.EfCore
{
    /// <summary>
    /// 扩展集合功能
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public static async Task<PagedList<T>> PagingAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize) where T:Entity
        {
            if (pageIndex < 0)
                throw new ArgumentException("页码不能小于0!", "pageIndex");
            if (pageSize <= 1)
                throw new ArgumentException("页值不能小于1!", "pageSize");

            var pagedQuery = source
                .Skip(pageIndex* pageSize)
                .Take(pageSize);

            return new PagedList<T>
            {
                Rows = await pagedQuery.ToListAsync(),
                Records = await source.CountAsync(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }
    }
}
