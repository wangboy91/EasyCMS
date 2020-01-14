using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.MySql.EfCore.AdminModule.Config
{
    public class PageViewConfig : IEntityTypeConfiguration<PageViewEntity>
    {
        public void Configure(EntityTypeBuilder<PageViewEntity> builder)
        {
            builder.ToTable("admin_pageview");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.UserId).HasColumnName("user_id");
            builder.Property(r => r.LoginName).HasColumnName("login_name").HasMaxLength(50);
            builder.Property(r => r.Url).HasColumnName("url").HasMaxLength(100);
            builder.Property(r => r.IP).HasColumnName("ip").HasMaxLength(50);
        }
    }
}
