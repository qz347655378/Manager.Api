using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Account",
                table: "UserInfo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RoleInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 6, 7, 18, 58, 16, 172, DateTimeKind.Local).AddTicks(4694), new DateTime(2021, 6, 7, 18, 58, 16, 172, DateTimeKind.Local).AddTicks(4698) });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 6, 7, 18, 58, 16, 170, DateTimeKind.Local).AddTicks(5717), new DateTime(2021, 6, 7, 18, 58, 16, 171, DateTimeKind.Local).AddTicks(3820) });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_Account",
                table: "UserInfo",
                column: "Account",
                unique: true,
                filter: "[Account] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInfo_Account",
                table: "UserInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Account",
                table: "UserInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RoleInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 6, 7, 18, 0, 10, 864, DateTimeKind.Local).AddTicks(9609), new DateTime(2021, 6, 7, 18, 0, 10, 864, DateTimeKind.Local).AddTicks(9613) });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateTime", "EditTime" },
                values: new object[] { new DateTime(2021, 6, 7, 18, 0, 10, 862, DateTimeKind.Local).AddTicks(9855), new DateTime(2021, 6, 7, 18, 0, 10, 863, DateTimeKind.Local).AddTicks(8121) });
        }
    }
}
