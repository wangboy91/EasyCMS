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
    /// 用户角色关系实体
    /// </summary>
    public class UserRoleEntity : Entity
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual RoleEntity Role { get; set; }
    }
}
