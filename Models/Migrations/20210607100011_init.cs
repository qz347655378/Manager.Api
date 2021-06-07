using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    ActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionStatus = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false)
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
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                values: new object[] { 1, new DateTime(2021, 6, 7, 18, 0, 10, 864, DateTimeKind.Local).AddTicks(9609), new DateTime(2021, 6, 7, 18, 0, 10, 864, DateTimeKind.Local).AddTicks(9613), 0, "Administrator", 0 });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "Id", "Account", "AccountStatus", "CreateTime", "EditTime", "Ip", "IsDelete", "Mobile", "Nickname", "Password", "RoleId", "UserType" },
                values: new object[] { 1, "admin", 1, new DateTime(2021, 6, 7, 18, 0, 10, 862, DateTimeKind.Local).AddTicks(9855), new DateTime(2021, 6, 7, 18, 0, 10, 863, DateTimeKind.Local).AddTicks(8121), null, 0, "", "Administrator", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_RoleAction_ActionId",
                table: "RoleAction",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAction_RoleId",
                table: "RoleAction",
                column: "RoleId");

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
