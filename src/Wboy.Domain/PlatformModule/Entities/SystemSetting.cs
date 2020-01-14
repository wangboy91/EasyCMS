using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Domain.PlatformModule.Entities
{
    public class SystemSetting : Entity
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
        /// 添加人
        /// </summary>
        public Guid? AddUserId { set; get; }
        /// <summary>
        /// 最近编辑人
        /// </summary>
        public Guid? UpdateUserId { set; get; }
        /// <summary>
        /// 最近编辑时间
        /// </summary>
        public DateTime? UpdateTime { set; get; }
    }
}
