using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.MySql.EfCore.AdminModule.Config
{
    public class AreaConfig : IEntityTypeConfiguration<AreaEntity>
    {
        public void Configure(EntityTypeBuilder<AreaEntity> builder)
        {
            builder.ToTable("admin_area");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.Name).HasColumnName("name").HasMaxLength(50);
            
        }
    }
}
