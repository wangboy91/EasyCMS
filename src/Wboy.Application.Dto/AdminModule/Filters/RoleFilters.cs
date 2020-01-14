using System;
using System.Collections.Generic;
using Wboy.Application.Dto.AdminModule.Filters;

namespace Wboy.Application.Dto.AdminModule.Filters
{
    /// <summary>
    /// 角色搜索过滤器
    /// </summary>
    public class RoleFilters : BaseFilter
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 是否排除当前UserId拥有的角色
        /// </summary>
        public bool ExcludeMyRoles { get; set; }
    }
}
