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


using System;

namespace Wboy.Domain.AdminModule.Entities
{
    /// <summary>
    /// 用户登录日志实体
    /// </summary>
    public class LoginLogEntity : Entity
    {
        public virtual UserEntity User { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 登录结果信息
        /// </summary>
        public string Message { get; set; }
    }
}
