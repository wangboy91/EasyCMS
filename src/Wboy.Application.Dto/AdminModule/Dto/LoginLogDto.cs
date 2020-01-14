﻿using System;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.Application.Dto.AdminModule.Dto
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLogDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { get; set; }

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

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        public LoginLogDto() { }

        public LoginLogDto(LoginLogEntity entity)
        {
            Id = entity.Id;
            UserId = entity.UserId;
            LoginName = entity.LoginName;
            IP = entity.IP;
            Message = entity.Message;
            CreateDateTime = entity.AddTime;
        }
    }
}
