using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Category",
                newName: "IsDeleted");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 1, false, "Categoria Base" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Category",
                newName: "Status");
        }
    }
}
