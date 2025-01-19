using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCompany.Migrations
{
    /// <inheritdoc />
    public partial class Create2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADE0CE87-9FC6-4099-8A8F-64991AD78D5D",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0fd1403-2da6-41c3-a1df-6fe7e9d13df6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7e3f0b89-b1d2-4368-9418-63a68b0c6883", "AQAAAAIAAYagAAAAEIi4WkXsCsn+bD7cqOkyX8LV7J0bcvzgamUWOb16zzGgfVo0ZOVQcjYFbc9EDeYbng==" });

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("31ed769f-5aa3-43c3-97ed-cda0cf820cd3"),
                columns: new[] { "DateAdded", "Text" },
                values: new object[] { new DateTime(2025, 1, 18, 23, 49, 29, 452, DateTimeKind.Utc).AddTicks(8158), "Содержание заполняется администратором" });

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("75923b22-ddba-412e-a03c-2d6cccde2931"),
                columns: new[] { "DateAdded", "Text" },
                values: new object[] { new DateTime(2025, 1, 18, 23, 49, 29, 452, DateTimeKind.Utc).AddTicks(8132), "Содержание заполняется администратором" });

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("ade06cc3-181c-41ea-a1dd-d98e825c01a9"),
                columns: new[] { "DateAdded", "Text" },
                values: new object[] { new DateTime(2025, 1, 18, 23, 49, 29, 452, DateTimeKind.Utc).AddTicks(8171), "Содержание заполняется администратором" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADE0CE87-9FC6-4099-8A8F-64991AD78D5D",
                column: "ConcurrencyStamp",
                value: "ab3c35b4-dee0-4d02-b261-322a6008a00a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0fd1403-2da6-41c3-a1df-6fe7e9d13df6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6467f0fc-357e-44bc-80bf-15365a99ced6", "AQAAAAEAACcQAAAAECWcnGnIulXFsKltZKcrlBaMx7/8bE8jAtcx0mtWZDwdWRqxfRjoMwH2th8KN02dZQ==" });

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("31ed769f-5aa3-43c3-97ed-cda0cf820cd3"),
                columns: new[] { "DateAdded", "Text" },
                values: new object[] { new DateTime(2024, 1, 7, 21, 27, 6, 455, DateTimeKind.Utc).AddTicks(6005), "Содержание заполняется адмиристратором" });

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("75923b22-ddba-412e-a03c-2d6cccde2931"),
                columns: new[] { "DateAdded", "Text" },
                values: new object[] { new DateTime(2024, 1, 7, 21, 27, 6, 455, DateTimeKind.Utc).AddTicks(6005), "Содержание заполняется адмиристратором" });

            migrationBuilder.UpdateData(
                table: "TextFields",
                keyColumn: "Id",
                keyValue: new Guid("ade06cc3-181c-41ea-a1dd-d98e825c01a9"),
                columns: new[] { "DateAdded", "Text" },
                values: new object[] { new DateTime(2024, 1, 7, 21, 27, 6, 455, DateTimeKind.Utc).AddTicks(6005), "Содержание заполняется адмиристратором" });
        }
    }
}
