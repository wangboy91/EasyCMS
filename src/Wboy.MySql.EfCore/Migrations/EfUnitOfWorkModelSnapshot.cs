﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wboy.MySql.EfCore;

namespace Wboy.MySql.EfCore.Migrations
{
    [DbContext(typeof(EfUnitOfWork))]
    partial class EfUnitOfWorkModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.EntityFrameworkCore.AutoHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Changed");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Kind");

                    b.Property<string>("RowId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("AutoHistory");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.AdminConfigEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<DateTime>("DataInitedDate")
                        .HasColumnName("data_init_date");

                    b.Property<bool>("IsDataInited")
                        .HasColumnName("is_data_inited");

                    b.Property<bool>("IsDelete")
                        .HasColumnName("is_delete");

                    b.Property<string>("SystemName")
                        .HasColumnName("system_name");

                    b.HasKey("Id");

                    b.ToTable("admin_config");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.AreaEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("admin_area");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.LoginLogEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<string>("IP")
                        .HasColumnName("ip")
                        .HasMaxLength(50);

                    b.Property<string>("LoginName")
                        .HasColumnName("login_name")
                        .HasMaxLength(50);

                    b.Property<string>("Message")
                        .HasColumnName("message")
                        .HasMaxLength(500);

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("admin_login_log");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.MenuEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<string>("Code")
                        .HasColumnName("code")
                        .HasMaxLength(50);

                    b.Property<string>("Icon")
                        .HasColumnName("icon")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(50);

                    b.Property<int>("Order")
                        .HasColumnName("order");

                    b.Property<Guid?>("ParentId")
                        .HasColumnName("parent_id");

                    b.Property<string>("PathCode")
                        .HasColumnName("path_code")
                        .HasMaxLength(50);

                    b.Property<byte>("Type")
                        .HasColumnName("type");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("admin_menu");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.PageViewEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<string>("IP")
                        .HasColumnName("ip")
                        .HasMaxLength(50);

                    b.Property<string>("LoginName")
                        .HasColumnName("login_name")
                        .HasMaxLength(50);

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasMaxLength(100);

                    b.Property<Guid?>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("admin_pageview");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.PathCodeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<string>("Code")
                        .HasColumnName("code")
                        .HasMaxLength(50);

                    b.Property<int>("Len")
                        .HasColumnName("len");

                    b.HasKey("Id");

                    b.ToTable("admin_pathcode");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("admin_role");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.RoleMenuEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<Guid>("MenuId")
                        .HasColumnName("menu_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("RoleId");

                    b.ToTable("admin_role_menu");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<Guid>("AddUserId")
                        .HasColumnName("add_user_id");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(100);

                    b.Property<bool>("IsDelete")
                        .HasColumnName("is_delete");

                    b.Property<bool>("IsSuperMan")
                        .HasColumnName("is_superMan");

                    b.Property<string>("LoginName")
                        .HasColumnName("login_name")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasMaxLength(100);

                    b.Property<string>("RealName")
                        .HasColumnName("real_name")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("update_time");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnName("update_user_id");

                    b.HasKey("Id");

                    b.ToTable("admin_user");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.UserRoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<Guid>("RoleId")
                        .HasColumnName("role_id");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("admin_user_role");
                });

            modelBuilder.Entity("Wboy.Domain.PlatformModule.Entities.OssFileEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<Guid>("AddUserId")
                        .HasColumnName("add_user_id");

                    b.Property<string>("Describe")
                        .HasColumnName("describe")
                        .HasMaxLength(500);

                    b.Property<string>("FileExtension")
                        .HasColumnName("file_extension")
                        .HasMaxLength(10);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnName("file_name")
                        .HasMaxLength(50);

                    b.Property<int>("FileSize")
                        .HasColumnName("file_size");

                    b.Property<int>("FileType")
                        .HasColumnName("file_type");

                    b.Property<Guid?>("FolderId")
                        .HasColumnName("folder_id");

                    b.Property<bool>("IsDelete")
                        .HasColumnName("is_delete");

                    b.Property<string>("SavePath")
                        .IsRequired()
                        .HasColumnName("save_path")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("update_time");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnName("update_user_id");

                    b.HasKey("Id");

                    b.ToTable("oss_file");
                });

            modelBuilder.Entity("Wboy.Domain.PlatformModule.Entities.OssFolderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<Guid>("AddUserId")
                        .HasColumnName("add_user_id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("update_time");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnName("update_user_id");

                    b.HasKey("Id");

                    b.ToTable("oss_folder");
                });

            modelBuilder.Entity("Wboy.Domain.PlatformModule.Entities.SystemSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddTime")
                        .HasColumnName("add_time");

                    b.Property<Guid?>("AddUserId")
                        .HasColumnName("add_user_Id");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnName("data_type")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(500);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnName("setting_key")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("update_time");

                    b.Property<Guid?>("UpdateUserId")
                        .HasColumnName("update_user_Id");

                    b.Property<string>("Value")
                        .HasColumnName("setting_value")
                        .HasMaxLength(8000);

                    b.HasKey("Id");

                    b.ToTable("sys_setting");
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.LoginLogEntity", b =>
                {
                    b.HasOne("Wboy.Domain.AdminModule.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.RoleMenuEntity", b =>
                {
                    b.HasOne("Wboy.Domain.AdminModule.Entities.MenuEntity", "Menu")
                        .WithMany("RoleMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wboy.Domain.AdminModule.Entities.RoleEntity", "Role")
                        .WithMany("RoleMenus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wboy.Domain.AdminModule.Entities.UserRoleEntity", b =>
                {
                    b.HasOne("Wboy.Domain.AdminModule.Entities.RoleEntity", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wboy.Domain.AdminModule.Entities.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}