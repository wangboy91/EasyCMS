﻿
using System;
using Wboy.Domain.AdminModule;

namespace Wboy.Application.Dto.AdminModule.Filters
{
    /// <summary>
    /// 菜单搜索过滤器
    /// </summary>
    public class MenuFilters : BaseFilter
    {
        /// <summary>
        /// 排除的类型
        /// </summary>
        public int? ExcludeType { get; set; }

        public Guid? ParentId { get; set; }
    }
}
