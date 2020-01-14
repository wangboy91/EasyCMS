using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wboy.Domain.PlatformModule.Entities;

namespace Wboy.MySql.EfCore.PlatformModule.Config
{
    /// <summary>
    /// 文件 实体映射
    /// </summary>
    public class OssFileConfig : IEntityTypeConfiguration<OssFileEntity>
    {
        public void Configure(EntityTypeBuilder<OssFileEntity> builder)
        {
            builder.ToTable("oss_file");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.AddUserId).HasColumnName("add_user_id");
            builder.Property(r => r.UpdateUserId).HasColumnName("update_user_id");
            builder.Property(r => r.UpdateTime).HasColumnName("update_time");
            builder.Property(x => x.SavePath).HasColumnName("save_path").HasMaxLength(500).IsRequired();
            builder.Property(x => x.FileName).HasColumnName("file_name").HasMaxLength(50).IsRequired();
            builder.Property(x => x.FileType).HasColumnName("file_type").IsRequired();
            builder.Property(x => x.FolderId).HasColumnName("folder_id");
            builder.Property(x => x.FileExtension).HasColumnName("file_extension").HasMaxLength(10);
            builder.Property(x => x.Describe).HasColumnName("describe").HasMaxLength(500);
            builder.Property(x => x.FileSize).HasColumnName("file_size").IsRequired();
            builder.Property(x => x.IsDelete).HasColumnName("is_delete").IsRequired();
        }
    }
}
