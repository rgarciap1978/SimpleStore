using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 2, 12, 35, 3, 251, DateTimeKind.Local).AddTicks(7682));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 2, 12, 35, 3, 254, DateTimeKind.Local).AddTicks(4909));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 27, 13, 10, 8, 574, DateTimeKind.Local).AddTicks(31));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 27, 13, 10, 8, 577, DateTimeKind.Local).AddTicks(971));
        }
    }
}
