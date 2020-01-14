using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wboy.Domain.AdminModule.Entities;

namespace Wboy.MySql.EfCore.AdminModule.Config
{
    public class PathCodeConfig : IEntityTypeConfiguration<PathCodeEntity>
    {
        public void Configure(EntityTypeBuilder<PathCodeEntity> builder)
        {
            builder.ToTable("admin_pathcode");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.AddTime).HasColumnName("add_time");
            builder.Property(r => r.Code).HasColumnName("code").HasMaxLength(50);
            builder.Property(r => r.Len).HasColumnName("len");

        }
    }
}
