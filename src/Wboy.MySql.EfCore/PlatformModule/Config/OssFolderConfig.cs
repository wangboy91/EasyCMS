using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wboy.Domain.PlatformModule.Entities;

namespace Wboy.MySql.EfCore.PlatformModule.Config
{
    /// <summary>
    /// 文件夹 实体映射
    /// </summary>
    public class OssFolderConfig : IEntityTypeConfiguration<OssFolderEntity>
    {
        public void Configure(EntityTypeBuilder<OssFolderEntity> builder)
        {
            builder.ToTable("oss_folder");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.AddUserId).HasColumnName("add_user_id");
            builder.Property(r => r.UpdateUserId).HasColumnName("update_user_id");
            builder.Property(r => r.UpdateTime).HasColumnName("update_time");
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(500).IsRequired();
            builder.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
        }
    }
}
