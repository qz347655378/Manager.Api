using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class AddMenuInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuAction",
                columns: new[] { "Id", "ActionName", "ActionStatus", "ActionType", "ActionUrl", "Code", "CreateTime", "EditTime", "Icon", "IsDelete", "ParentId", "Sort" },
                values: new object[,]
                {
                    { 1, "系统管理", 1, 1, "/", "System", new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(5679), new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(5926), "layui-icon-set", 0, 0, 1 },
                    { 2, "用户管理", 1, 1, "/System/User", "System.User", new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7026), new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7033), "", 0, 1, 1 },
                    { 3, "角色管理", 1, 1, "/System/Role", "System.Role", new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7072), new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7072), "", 0, 1, 2 },
                    { 4, "菜单管理", 1, 1, "/System/Menu", "System.Role", new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7075), new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7075), "", 0, 1, 3 }
                });

            migrationBuilder.UpdateData(
                table: "RoleInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(3698), new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(3701) });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 11, 17, 56, 53, 566, DateTimeKind.Local).AddTicks(3754), new DateTime(2021, 9, 11, 17, 56, 53, 567, DateTimeKind.Local).AddTicks(2524) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "RoleInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 11, 11, 28, 33, 424, DateTimeKind.Local).AddTicks(9023), new DateTime(2021, 9, 11, 11, 28, 33, 424, DateTimeKind.Local).AddTicks(9023) });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 11, 11, 28, 33, 423, DateTimeKind.Local).AddTicks(108), new DateTime(2021, 9, 11, 11, 28, 33, 423, DateTimeKind.Local).AddTicks(8180) });
        }
    }
}
