using System;
using System.ComponentModel.DataAnnotations;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.Application.Dto.AdminModule.Dto
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public RoleDto() { }
        public RoleDto(RoleEntity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

    }
}
