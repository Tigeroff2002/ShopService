using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations;

/// <inheritdoc />
public partial class SeedProductsData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
            values: new object[] { 1, 1, "Iphone13", 2, 60000.0, 6.3f, "2400x1400", 32f, 3356f, 6f });
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
            values: new object[] { 2, 2, "Samsung Galaxy Tab G", 1, 20000.0, 9.1f, "1920x1080", 16f, 4200f, 4f });
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
            values: new object[] { 3, 3, "Lenovo Ideapad Gaming 3", 5, 60000.0, 15.6f, "1920x1080", null, 10000f, 8f });
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
            values: new object[] { 4, 7, "LG BlackSync 3 FullHd", 4, 8000.0, 22f, "1920x1080", null, null, null });
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
            values: new object[] { 5, 1, "Iphone8", 2, 20000.0, 4.7f, "1337x800", 16f, 1900f, 3f });
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
            values: new object[] { 6, 2, "Ipad Air 2", 2, 50000.0, 9.7f, "2156x1140", 16f, 3700f, 6f });
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
            values: new object[] { 7, 10, "Mi Buds 3", 3, 3.0, null, null, null, null, null });
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
            values: new object[] { 8, 1, "Redmi 9 Note Pro", 3, 20000.0, 6.7f, "1920x1080", 32f, 3500f, 4f });
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
            values: new object[] { 9, 3, "MacBook 10 Pro", 2, 110000.0, 14f, "2400x1400", null, 9000f, 12f });
        migrationBuilder.InsertData(
            table: "Products",
            columns: new[] { "Id", "DeviceTypeId", "Name", "ProducerId", "Cost", "Size", "ScreenResolution", "CamMp", "AccumCapacity", "RAM" },
            values: new object[] { 10, 1, "LG Velvet G900", 4, 18000.0, 6.9f, "1920x1080", 16f, 3500f, 4f });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
                       table: "Products",
                       keyColumn: "Id",
                       keyValue: 1);
        migrationBuilder.DeleteData(
                       table: "Products",
                       keyColumn: "Id",
                       keyValue: 2);
        migrationBuilder.DeleteData(
                       table: "Products",
                       keyColumn: "Id",
                       keyValue: 3);
        migrationBuilder.DeleteData(
                       table: "Products",
                       keyColumn: "Id",
                       keyValue: 4);
        migrationBuilder.DeleteData(
                       table: "Products",
                       keyColumn: "Id",
                       keyValue: 5);
        migrationBuilder.DeleteData(
                       table: "Products",
                       keyColumn: "Id",
                       keyValue: 6);
        migrationBuilder.DeleteData(
                       table: "Products",
                       keyColumn: "Id",
                       keyValue: 7);
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
    }
}
