using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class UserInfo_add_lastLoginTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "UserInfo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 12, 0, 6, 43, 533, DateTimeKind.Local).AddTicks(9508), new DateTime(2021, 9, 12, 0, 6, 43, 533, DateTimeKind.Local).AddTicks(9745) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 12, 0, 6, 43, 534, DateTimeKind.Local).AddTicks(770), new DateTime(2021, 9, 12, 0, 6, 43, 534, DateTimeKind.Local).AddTicks(778) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 12, 0, 6, 43, 534, DateTimeKind.Local).AddTicks(816), new DateTime(2021, 9, 12, 0, 6, 43, 534, DateTimeKind.Local).AddTicks(817) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 12, 0, 6, 43, 534, DateTimeKind.Local).AddTicks(820), new DateTime(2021, 9, 12, 0, 6, 43, 534, DateTimeKind.Local).AddTicks(821) });

            migrationBuilder.UpdateData(
                table: "RoleInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime", "RoleStatus" },
                values: new object[] { new DateTime(2021, 9, 12, 0, 6, 43, 533, DateTimeKind.Local).AddTicks(7432), new DateTime(2021, 9, 12, 0, 6, 43, 533, DateTimeKind.Local).AddTicks(7440), 1 });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 12, 0, 6, 43, 529, DateTimeKind.Local).AddTicks(8172), new DateTime(2021, 9, 12, 0, 6, 43, 532, DateTimeKind.Local).AddTicks(6479) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "UserInfo");

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(5679), new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(5926) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7026), new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7033) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7072), new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7072) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7075), new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(7075) });

            migrationBuilder.UpdateData(
                table: "RoleInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime", "RoleStatus" },
                values: new object[] { new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(3698), new DateTime(2021, 9, 11, 17, 56, 53, 568, DateTimeKind.Local).AddTicks(3701), 0 });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 11, 17, 56, 53, 566, DateTimeKind.Local).AddTicks(3754), new DateTime(2021, 9, 11, 17, 56, 53, 567, DateTimeKind.Local).AddTicks(2524) });
        }
    }
}
