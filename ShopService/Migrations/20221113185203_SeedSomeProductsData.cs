using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopService.Migrations
{
    /// <inheritdoc />
    public partial class SeedSomeProductsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
                values: new object[] { 8, 1, "Iphone13", 2, 60000.0, 6.3f, "2400x1400", 32f, 3356f, 6f });
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
                values: new object[] { 9, 2, "Samsung Galaxy Tab G", 1, 20000.0, 9.1f, "1920x1080", 16f, 4200f, 4f });
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
                values: new object[] { 10, 3, "Lenovo Ideapad Gaming 3", 5, 60000.0, 15.6f, "1920x1080", null, 10000f, 8f });
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
                values: new object[] { 11, 7, "LG BlackSync 3 FullHd", 4, 8000.0, 22f, "1920x1080", null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                           table: "Products",
                           keyColumn: "Id",
                           keyValue: 8);
            migrationBuilder.DeleteData(
                           table: "Products",
                           keyColumn: "Id",
                           keyValue: 9);
            migrationBuilder.DeleteData(
                           table: "Products",
                           keyColumn: "Id",
                           keyValue: 10);
            migrationBuilder.DeleteData(
                           table: "Products",
                           keyColumn: "Id",
                           keyValue: 11);
        }
    }
}
