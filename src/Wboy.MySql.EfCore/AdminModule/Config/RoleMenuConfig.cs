using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.MySql.EfCore.AdminModule.Config
{
    public class RoleMenuConfig : IEntityTypeConfiguration<RoleMenuEntity>
    {
        public void Configure(EntityTypeBuilder<RoleMenuEntity> builder)
        {
            builder.ToTable("admin_role_menu");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.RoleId).HasColumnName("role_id");
            builder.Property(r => r.MenuId).HasColumnName("menu_id");
            builder.HasOne(x => x.Menu).WithMany(x=>x.RoleMenus).HasForeignKey(x => x.MenuId);
            builder.HasOne(x => x.Role).WithMany(x => x.RoleMenus).HasForeignKey(x => x.RoleId);
        }
    }
}
