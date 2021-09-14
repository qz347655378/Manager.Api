using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class fixSomeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 18, 57, 22, DateTimeKind.Local).AddTicks(1396), new DateTime(2021, 9, 14, 15, 18, 57, 22, DateTimeKind.Local).AddTicks(1643) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 18, 57, 22, DateTimeKind.Local).AddTicks(2732), new DateTime(2021, 9, 14, 15, 18, 57, 22, DateTimeKind.Local).AddTicks(2863) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 18, 57, 22, DateTimeKind.Local).AddTicks(2919), new DateTime(2021, 9, 14, 15, 18, 57, 22, DateTimeKind.Local).AddTicks(2919) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 18, 57, 22, DateTimeKind.Local).AddTicks(2923), new DateTime(2021, 9, 14, 15, 18, 57, 22, DateTimeKind.Local).AddTicks(2923) });

            migrationBuilder.UpdateData(
                table: "RoleInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 18, 57, 21, DateTimeKind.Local).AddTicks(9213), new DateTime(2021, 9, 14, 15, 18, 57, 21, DateTimeKind.Local).AddTicks(9217) });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 18, 57, 19, DateTimeKind.Local).AddTicks(9629), new DateTime(2021, 9, 14, 15, 18, 57, 20, DateTimeKind.Local).AddTicks(8169) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 17, 53, 317, DateTimeKind.Local).AddTicks(3254), new DateTime(2021, 9, 14, 15, 17, 53, 317, DateTimeKind.Local).AddTicks(3511) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 17, 53, 317, DateTimeKind.Local).AddTicks(4893), new DateTime(2021, 9, 14, 15, 17, 53, 317, DateTimeKind.Local).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 17, 53, 317, DateTimeKind.Local).AddTicks(4946), new DateTime(2021, 9, 14, 15, 17, 53, 317, DateTimeKind.Local).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 17, 53, 317, DateTimeKind.Local).AddTicks(4953), new DateTime(2021, 9, 14, 15, 17, 53, 317, DateTimeKind.Local).AddTicks(4953) });

            migrationBuilder.UpdateData(
                table: "RoleInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 17, 53, 317, DateTimeKind.Local).AddTicks(1032), new DateTime(2021, 9, 14, 15, 17, 53, 317, DateTimeKind.Local).AddTicks(1043) });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 14, 15, 17, 53, 314, DateTimeKind.Local).AddTicks(8282), new DateTime(2021, 9, 14, 15, 17, 53, 315, DateTimeKind.Local).AddTicks(8127) });
        }
    }
}
