using System.ComponentModel.DataAnnotations;
using System;
using Wboy.Domain.AdminModule;
using Wboy.Infrastructure.Core.Extension;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.Application.Dto.AdminModule.Dto
{
    /// <summary>
    /// 菜单模型
    /// </summary>
    public class MenuDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 上级菜单名称
        /// </summary>
        public string ParentName { get; set; }

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
        [Display(Name = "排序数字")]
        public int Order { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 菜单类型名称
        /// </summary>
        public string TypeName
        {
            get { return ((MenuType)Type).GetDescriptionForEnum(); }
        }

        public MenuDto() { }

        public MenuDto(MenuEntity entity)
        {
            Id = entity.Id;
            ParentId = entity.ParentId;
            Name = entity.Name;
            Url = entity.Url;
            Order = entity.Order;
            Type = (int)entity.Type;
            Icon = entity.Icon;
        }
    }
}
