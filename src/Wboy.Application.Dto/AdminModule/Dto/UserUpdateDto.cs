using System;
using System.ComponentModel.DataAnnotations;

namespace Wboy.Application.Dto.AdminModule.Dto
{
    /// <summary>
    /// 用户更新DTO
    /// </summary>
    public class UserUpdateDto
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
    }
}
