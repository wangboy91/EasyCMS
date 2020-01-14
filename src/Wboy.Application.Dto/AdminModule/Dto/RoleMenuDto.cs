using System;

namespace Wboy.Application.Dto.AdminModule.Dto
{
    public class RoleMenuDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { get; set; }
    }
}
