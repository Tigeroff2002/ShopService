using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopService.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Producer",
                columns: new[] { "Id", "Country", "Name", "Popularity", "WebSite" },
                values: new object[] { 1, "Вьетнам", "Samsung", 4.9f, "www.samsung.com/ru" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Producer",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
