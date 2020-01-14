using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.MySql.EfCore.AdminModule.Config
{
    public class LoginLogConfig : IEntityTypeConfiguration<LoginLogEntity>
    {
        public void Configure(EntityTypeBuilder<LoginLogEntity> builder)
        {
            builder.ToTable("admin_login_log");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.UserId).HasColumnName("user_id");
            builder.Property(r => r.LoginName).HasColumnName("login_name").HasMaxLength(50);
            builder.Property(r => r.IP).HasColumnName("ip").HasMaxLength(50);
            builder.Property(r => r.Message).HasColumnName("message").HasMaxLength(500);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
