using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.MySql.EfCore.AdminModule.Config
{
    public class UserConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("admin_user");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.LoginName).HasColumnName("login_name").HasMaxLength(50);
            builder.Property(r => r.RealName).HasColumnName("real_name").HasMaxLength(50);
            builder.Property(r => r.Email).HasColumnName("email").HasMaxLength(100);
            builder.Property(r => r.Password).HasColumnName("password").HasMaxLength(100);
            builder.Property(r => r.IsSuperMan).HasColumnName("is_superMan");
            builder.Property(r => r.AddUserId).HasColumnName("add_user_id");
            builder.Property(r => r.UpdateUserId).HasColumnName("update_user_id");
            builder.Property(r => r.UpdateTime).HasColumnName("update_time");
            builder.Property(r => r.IsDelete).HasColumnName("is_delete");
        }
    }
}
