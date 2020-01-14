using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wboy.MySql.EfCore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin_area",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admin_config",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    system_name = table.Column<string>(nullable: true),
                    is_data_inited = table.Column<bool>(nullable: false),
                    data_init_date = table.Column<DateTime>(nullable: false),
                    is_delete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_config", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admin_menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    parent_id = table.Column<Guid>(nullable: true),
                    code = table.Column<string>(maxLength: 50, nullable: true),
                    path_code = table.Column<string>(maxLength: 50, nullable: true),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    url = table.Column<string>(maxLength: 100, nullable: true),
                    order = table.Column<int>(nullable: false),
                    type = table.Column<byte>(nullable: false),
                    icon = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admin_pageview",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<Guid>(nullable: true),
                    login_name = table.Column<string>(maxLength: 50, nullable: true),
                    url = table.Column<string>(maxLength: 100, nullable: true),
                    ip = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_pageview", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admin_pathcode",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    code = table.Column<string>(maxLength: 50, nullable: true),
                    len = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_pathcode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admin_role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admin_user",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    add_user_id = table.Column<Guid>(nullable: false),
                    update_user_id = table.Column<Guid>(nullable: true),
                    update_time = table.Column<DateTime>(nullable: true),
                    login_name = table.Column<string>(maxLength: 50, nullable: true),
                    real_name = table.Column<string>(maxLength: 50, nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: true),
                    password = table.Column<string>(maxLength: 100, nullable: true),
                    is_superMan = table.Column<bool>(nullable: false),
                    is_delete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutoHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RowId = table.Column<string>(maxLength: 50, nullable: false),
                    TableName = table.Column<string>(maxLength: 128, nullable: false),
                    Changed = table.Column<string>(nullable: true),
                    Kind = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "oss_file",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    add_user_id = table.Column<Guid>(nullable: false),
                    update_user_id = table.Column<Guid>(nullable: true),
                    update_time = table.Column<DateTime>(nullable: true),
                    folder_id = table.Column<Guid>(nullable: true),
                    save_path = table.Column<string>(maxLength: 500, nullable: false),
                    file_name = table.Column<string>(maxLength: 50, nullable: false),
                    file_type = table.Column<int>(nullable: false),
                    describe = table.Column<string>(maxLength: 500, nullable: true),
                    file_size = table.Column<int>(nullable: false),
                    is_delete = table.Column<bool>(nullable: false),
                    file_extension = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oss_file", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "oss_folder",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    add_user_id = table.Column<Guid>(nullable: false),
                    update_user_id = table.Column<Guid>(nullable: true),
                    update_time = table.Column<DateTime>(nullable: true),
                    name = table.Column<string>(maxLength: 500, nullable: false),
                    code = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oss_folder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sys_setting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    setting_key = table.Column<string>(maxLength: 100, nullable: false),
                    setting_value = table.Column<string>(maxLength: 8000, nullable: true),
                    data_type = table.Column<string>(maxLength: 100, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: true),
                    add_user_Id = table.Column<Guid>(nullable: true),
                    update_user_Id = table.Column<Guid>(nullable: true),
                    update_time = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "admin_role_menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    role_id = table.Column<Guid>(nullable: false),
                    menu_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_role_menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_admin_role_menu_admin_menu_menu_id",
                        column: x => x.menu_id,
                        principalTable: "admin_menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_admin_role_menu_admin_role_role_id",
                        column: x => x.role_id,
                        principalTable: "admin_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "admin_login_log",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    login_name = table.Column<string>(maxLength: 50, nullable: true),
                    ip = table.Column<string>(maxLength: 50, nullable: true),
                    message = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_login_log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_admin_login_log_admin_user_user_id",
                        column: x => x.user_id,
                        principalTable: "admin_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "admin_user_role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    add_time = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    role_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_user_role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_admin_user_role_admin_role_role_id",
                        column: x => x.role_id,
                        principalTable: "admin_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_admin_user_role_admin_user_user_id",
                        column: x => x.user_id,
                        principalTable: "admin_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admin_login_log_user_id",
                table: "admin_login_log",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_admin_role_menu_menu_id",
                table: "admin_role_menu",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_admin_role_menu_role_id",
                table: "admin_role_menu",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_admin_user_role_role_id",
                table: "admin_user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_admin_user_role_user_id",
                table: "admin_user_role",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_area");

            migrationBuilder.DropTable(
                name: "admin_config");

            migrationBuilder.DropTable(
                name: "admin_login_log");

            migrationBuilder.DropTable(
                name: "admin_pageview");

            migrationBuilder.DropTable(
                name: "admin_pathcode");

            migrationBuilder.DropTable(
                name: "admin_role_menu");

            migrationBuilder.DropTable(
                name: "admin_user_role");

            migrationBuilder.DropTable(
                name: "AutoHistory");

            migrationBuilder.DropTable(
                name: "oss_file");

            migrationBuilder.DropTable(
                name: "oss_folder");

            migrationBuilder.DropTable(
                name: "sys_setting");

            migrationBuilder.DropTable(
                name: "admin_menu");

            migrationBuilder.DropTable(
                name: "admin_role");

            migrationBuilder.DropTable(
                name: "admin_user");
        }
    }
}
