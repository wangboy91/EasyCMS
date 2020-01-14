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
using System.Collections.Generic;

namespace Wboy.Domain.AdminModule.Entities
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuEntity : Entity
    {
        /// <summary>
        /// 上级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 路径码=(上级的路径码+当前的Code)
        /// </summary>
        public string PathCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 排序越大越靠后
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 角色菜单关系
        /// </summary>
        public virtual IList<RoleMenuEntity> RoleMenus { get; set; }
    }
}
