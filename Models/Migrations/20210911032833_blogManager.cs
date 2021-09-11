using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class blogManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActionStatus = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleStatus = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleAction_MenuAction_ActionId",
                        column: x => x.ActionId,
                        principalTable: "MenuAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleAction_RoleInfo_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfo_RoleInfo_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RoleInfo",
                columns: new[] { "Id", "CreateTime", "EditTime", "IsDelete", "RoleName", "RoleStatus" },
                values: new object[] { 1, new DateTime(2021, 9, 11, 11, 28, 33, 424, DateTimeKind.Local).AddTicks(9023), new DateTime(2021, 9, 11, 11, 28, 33, 424, DateTimeKind.Local).AddTicks(9023), 0, "Administrator", 0 });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "Id", "Account", "AccountStatus", "CreateTime", "EditTime", "Ip", "IsDelete", "Mobile", "Nickname", "Password", "RoleId", "UserType" },
                values: new object[] { 1, "admin", 1, new DateTime(2021, 9, 11, 11, 28, 33, 423, DateTimeKind.Local).AddTicks(108), new DateTime(2021, 9, 11, 11, 28, 33, 423, DateTimeKind.Local).AddTicks(8180), null, 0, "", "Administrator", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_RoleAction_ActionId",
                table: "RoleAction",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAction_RoleId",
                table: "RoleAction",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleInfo_RoleName",
                table: "RoleInfo",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_Account",
                table: "UserInfo",
                column: "Account",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_RoleId",
                table: "UserInfo",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleAction");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "MenuAction");

            migrationBuilder.DropTable(
                name: "RoleInfo");
        }
    }
}
