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
    /// 用户实体
    /// </summary>
    public class PageViewEntity : Entity
    {

        /// <summary>
        /// UserId
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 访问者IP
        /// </summary>
        public string IP { get; set; }
    }
}
