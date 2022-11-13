using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopService.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Producers",
                columns: new[] { "Id", "Name", "Country", "Popularity", "WebSite" },
                values: new object[] { 1, "Samsung", "Вьетнам", 4.9f, "www.samsung.com" });
            migrationBuilder.InsertData(
                table: "Producers",
                columns: new[] { "Id", "Name", "Country", "Popularity", "WebSite" },
                values: new object[] { 2, "Apple", "США", 4.8f, "www.apple.com" });
            migrationBuilder.InsertData(
                table: "Producers",
                columns: new[] { "Id", "Name", "Country", "Popularity", "WebSite" },
                values: new object[] { 3, "Xiaomi", "Китай", 4.9f, "www.mi.com" });
            migrationBuilder.InsertData(
                table: "Producers",
                columns: new[] { "Id", "Name", "Country", "Popularity", "WebSite" },
                values: new object[] { 4, "LG", "Тайвань", 4.8f, "www.lg.com" });
            migrationBuilder.InsertData(
                table: "Producers",
                columns: new[] { "Id", "Name", "Country", "Popularity", "WebSite" },
                values: new object[] { 5, "Lenovo", "Китай", 4.7f, "www.lenovo.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1);
            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2);
            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3);
            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 4);
            migrationBuilder.DeleteData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
