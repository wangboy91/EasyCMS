using System;
using System.Collections.Generic;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.Application.Dto.AdminModule.Dto
{
    /// <summary>
    /// 用户DTO
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSuperMan { get; set; }
        /// <summary>
        /// 用户拥有的角色
        /// </summary>
        public virtual IList<UserRoleDto> UserRoles { get; set; }

        public UserDto() { }

        public UserDto(UserEntity entity)
        {
            Id = entity.Id;
            LoginName = entity.LoginName;
            RealName = entity.RealName;
            Email = entity.Email;
            IsDelete = entity.IsDelete;
            CreateDateTime = entity.AddTime;
            IsSuperMan = entity.IsSuperMan;
            //UserRoles = entity
        }
    }
}
