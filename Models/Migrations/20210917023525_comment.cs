using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Account",
                table: "UserInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "账号",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "MenuAction",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "MenuAction",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 17, 10, 35, 24, 738, DateTimeKind.Local).AddTicks(4804), new DateTime(2021, 9, 17, 10, 35, 24, 738, DateTimeKind.Local).AddTicks(5065) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 17, 10, 35, 24, 738, DateTimeKind.Local).AddTicks(6204), new DateTime(2021, 9, 17, 10, 35, 24, 738, DateTimeKind.Local).AddTicks(6211) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 17, 10, 35, 24, 738, DateTimeKind.Local).AddTicks(6254), new DateTime(2021, 9, 17, 10, 35, 24, 738, DateTimeKind.Local).AddTicks(6254) });

            migrationBuilder.UpdateData(
                table: "MenuAction",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 17, 10, 35, 24, 738, DateTimeKind.Local).AddTicks(6254), new DateTime(2021, 9, 17, 10, 35, 24, 738, DateTimeKind.Local).AddTicks(6257) });

            migrationBuilder.UpdateData(
                table: "RoleInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 17, 10, 35, 24, 738, DateTimeKind.Local).AddTicks(2392), new DateTime(2021, 9, 17, 10, 35, 24, 738, DateTimeKind.Local).AddTicks(2406) });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 9, 17, 10, 35, 24, 735, DateTimeKind.Local).AddTicks(2173), new DateTime(2021, 9, 17, 10, 35, 24, 736, DateTimeKind.Local).AddTicks(6871) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Account",
                table: "UserInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "账号");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "MenuAction",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "MenuAction",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
    }
}
