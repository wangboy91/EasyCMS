using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Domain.PlatformModule.Entities
{
    /// <summary>
    /// 文件夹 实体
    /// </summary>
    public class OssFolderEntity : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Code { get; set; }
    }
}
