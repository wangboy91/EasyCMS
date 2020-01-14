using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.MySql.EfCore.AdminModule.Config
{
    public class MenuConfig : IEntityTypeConfiguration<MenuEntity>
    {
        public void Configure(EntityTypeBuilder<MenuEntity> builder)
        {
            builder.ToTable("admin_menu");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.ParentId).HasColumnName("parent_id");
            builder.Property(r => r.Code).HasColumnName("code").HasMaxLength(50);
            builder.Property(r => r.PathCode).HasColumnName("path_code").HasMaxLength(50);
            builder.Property(r => r.Name).HasColumnName("name").HasMaxLength(50);
            builder.Property(r => r.Url).HasColumnName("url").HasMaxLength(100);
            builder.Property(r => r.Order).HasColumnName("order");
            builder.Property(r => r.Type).HasColumnName("type");
            builder.Property(r => r.Icon).HasColumnName("icon").HasMaxLength(50);
        }
    }
}
