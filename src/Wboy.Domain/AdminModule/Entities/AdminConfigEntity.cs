using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Domain.AdminModule.Entities
{
    /// <summary>
    /// 系统初始化配置
    /// </summary>
    public class AdminConfigEntity : Entity
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 数据初始化是否完成
        /// </summary>
        public bool IsDataInited { get; set; }
        /// <summary>
        /// 数据初始化时间
        /// </summary>
        public DateTime DataInitedDate { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
