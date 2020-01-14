using System;
using System.Collections.Generic;
using System.Text;
using Wboy.Domain.PlatformModule.Entities;

namespace Wboy.Application.Dto.AdminModule.Dto
{
    public class SystemSettingDto : BasicDto
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { set; get; }
        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { set; get; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { set; get; }
        /// <summary>
        /// 配置说明
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { set; get; }

        public SystemSettingDto() { }

        public SystemSettingDto(SystemSetting entity)
        {
            Id = entity.Id;
            Key = entity.Key;
            Value = entity.Value;
            DataType = entity.DataType;
            Description = entity.Description;
            AddTime = entity.AddTime;
            UpdateTime = entity.UpdateTime;
        }
    }
}
