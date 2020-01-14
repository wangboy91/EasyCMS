using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Domain.PlatformModule.Entities
{
    /// <summary>
    /// 文件夹统计(不映射数据表)
    /// </summary>
    public class OssFolderGroupEntity
    {
        /// <summary>
        /// 文件夹id
        /// </summary>
        public Guid? FolderId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
    }
}
