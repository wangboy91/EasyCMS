using System;
using Wboy.Domain.ValueObject;

namespace Wboy.Domain.PlatformModule.Entities
{
    /// <summary>
    /// 文件 实体
    /// </summary>
    public class OssFileEntity : BaseEntity
    {
        /// <summary>
        /// 文件夹Id
        /// </summary>
        public Guid? FolderId { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        public string SavePath { get; set;}
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileTypeEnum FileType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }
    }
}
