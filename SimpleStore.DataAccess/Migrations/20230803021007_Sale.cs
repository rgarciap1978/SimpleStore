using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Sale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Customer",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Correlative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sale_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 3, 12, 10, 7, 134, DateTimeKind.Local).AddTicks(1082));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 3, 12, 10, 7, 136, DateTimeKind.Local).AddTicks(7799));

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerId",
                table: "Sale",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_ProductId",
                table: "Sale",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Customer",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "getdate()");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 2, 12, 39, 55, 347, DateTimeKind.Local).AddTicks(8725));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 8, 2, 12, 39, 55, 351, DateTimeKind.Local).AddTicks(6788));
        }
    }
}
