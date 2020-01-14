using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wboy.Domain.PlatformModule.Entities;

namespace Wboy.MySql.EfCore.PlatformModule.Config
{
    public class SystemSettingConfig : IEntityTypeConfiguration<SystemSetting>
    {
        public void Configure(EntityTypeBuilder<SystemSetting> builder)
        {
            builder.ToTable("sys_setting");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.AddUserId).HasColumnName("add_user_Id");
            builder.Property(r => r.UpdateUserId).HasColumnName("update_user_Id");
            builder.Property(r => r.UpdateTime).HasColumnName("update_time");
            builder.Property(x => x.Key).HasColumnName("setting_key").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Value).HasColumnName("setting_value").HasMaxLength(8000);
            builder.Property(x => x.DataType).HasColumnName("data_type").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(500);
        }
    }
}
