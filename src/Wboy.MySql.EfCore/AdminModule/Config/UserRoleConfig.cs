using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.MySql.EfCore.AdminModule.Config
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable("admin_user_role");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.UserId).HasColumnName("user_id");
            builder.Property(r => r.RoleId).HasColumnName("role_id");
            builder.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
        }
    }
}
