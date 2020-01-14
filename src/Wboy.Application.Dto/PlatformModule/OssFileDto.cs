using System;
using Wboy.Domain.PlatformModule.Entities;

namespace Wboy.Application.Dto.PlatformModule
{
    public class OssFileLiteDto : BasicDto
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        public string SavePath { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }
        public OssFileLiteDto() { }
        public OssFileLiteDto(OssFileEntity entity)
        {
            Id = entity.Id;
            AddTime = entity.AddTime;
            SavePath = entity.SavePath;
            FileName = entity.FileName;
            FileExtension = entity.FileExtension;
        }
    }
    /// <summary>
    /// 文件夹 DTO
    /// </summary>
    public class OssFolderDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }

        public OssFolderDto() { }

        public OssFolderDto(OssFolderEntity entity, int number = 0)
        {
            Id = entity.Id;
            Name = entity.Name;
            Code = entity.Code;
            Number = number;
        }
    }
}
