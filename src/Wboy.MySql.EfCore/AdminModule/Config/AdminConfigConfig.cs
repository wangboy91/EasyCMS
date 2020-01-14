using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wboy.Domain.AdminModule.Entities;
using Wboy.Domain.PlatformModule.Entities;

namespace Wboy.MySql.EfCore.AdminModule.Config
{
    public class AdminConfigConfig:IEntityTypeConfiguration<AdminConfigEntity>
    {
        public void Configure(EntityTypeBuilder<AdminConfigEntity> builder)
        {
            builder.ToTable("admin_config");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.DataInitedDate).HasColumnName("data_init_date");
            builder.Property(r => r.IsDataInited).HasColumnName("is_data_inited");
            builder.Property(r => r.SystemName).HasColumnName("system_name");
            builder.Property(r => r.IsDelete).HasColumnName("is_delete");
        }
    }
}
