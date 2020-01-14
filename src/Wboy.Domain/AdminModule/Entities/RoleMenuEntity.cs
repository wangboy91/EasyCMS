/*******************************************************************************
* Copyright (C) 
* 
* Author: 
* Create Date: 
* Description: Automated building by  
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;

namespace Wboy.Domain.AdminModule.Entities
{
    /// <summary>
    /// 角色菜单关系实体
    /// </summary>
    public class RoleMenuEntity : Entity
    {

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual RoleEntity Role { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public virtual MenuEntity Menu { get; set; }
    }
}
