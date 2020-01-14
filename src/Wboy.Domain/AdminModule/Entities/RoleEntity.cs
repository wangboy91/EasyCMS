/*******************************************************************************
* Copyright (C) 
* 
* Author:
* Create Date: 
* Description:
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/


using System.Collections.Generic;

namespace Wboy.Domain.AdminModule.Entities
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public class RoleEntity : Entity
    {

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 用户角色关系
        /// </summary>
        public virtual IList<UserRoleEntity> UserRoles { get; set; } 

        /// <summary>
        /// 角色菜单关系
        /// </summary>
        public virtual IList<RoleMenuEntity> RoleMenus { get; set; } 
    }
}
