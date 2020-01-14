using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wboy.Infrastructure.Core;

// ReSharper disable once CheckNamespace
namespace WebApp
{
    public static class DataTableExtensions
    {
       

        public static JqGridModel<T> ToJqGridRows<T>(this PagedList<T> dtos)
        {
            return new JqGridModel<T>
            {
                Rows = dtos.Rows,
                Records = dtos.Records,
                Page = dtos.PageIndex+1,
                Pagesize = dtos.PageSize,
                Total = dtos.Total
            };
        }
    }
}
