using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations;

/// <inheritdoc />
public partial class SeedDeviceTypesData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "DeviceTypes",
            columns: new[] { "Id", "TypeEntity", "Name" },
            values: new object[] { 1, 1, "Smartphone" });
        migrationBuilder.InsertData(
            table: "DeviceTypes",
            columns: new[] { "Id", "TypeEntity", "Name" },
            values: new object[] { 2, 2, "TabletPad" });
        migrationBuilder.InsertData(
            table: "DeviceTypes",
            columns: new[] { "Id", "TypeEntity", "Name" },
            values: new object[] { 3, 3, "Notebook" });
        migrationBuilder.InsertData(
            table: "DeviceTypes",
            columns: new[] { "Id", "TypeEntity", "Name" },
            values: new object[] { 4, 4, "Laptop" });
        migrationBuilder.InsertData(
            table: "DeviceTypes",
            columns: new[] { "Id", "TypeEntity", "Name" },
            values: new object[] { 5, 5, "SystemBlock" });
        migrationBuilder.InsertData(
            table: "DeviceTypes",
            columns: new[] { "Id", "TypeEntity", "Name" },
            values: new object[] { 6, 6, "MonoBlock" });
        migrationBuilder.InsertData(
            table: "DeviceTypes",
            columns: new[] { "Id", "TypeEntity", "Name" },
            values: new object[] { 7, 7, "Monitor" });
        migrationBuilder.InsertData(
            table: "DeviceTypes",
            columns: new[] { "Id", "TypeEntity", "Name" },
            values: new object[] { 8, 8, "Mouse" });
        migrationBuilder.InsertData(
            table: "DeviceTypes",
            columns: new[] { "Id", "TypeEntity", "Name" },
            values: new object[] { 9, 9, "Keyboard" });
        migrationBuilder.InsertData(
            table: "DeviceTypes",
            columns: new[] { "Id", "TypeEntity", "Name" },
            values: new object[] { 10, 10, "Headphones" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "DeviceTypes",
            keyColumn: "Id",
            keyValue: 1);
        migrationBuilder.DeleteData(
            table: "DeviceTypes",
            keyColumn: "Id",
            keyValue: 2);
        migrationBuilder.DeleteData(
            table: "DeviceTypes",
            keyColumn: "Id",
            keyValue: 3);
        migrationBuilder.DeleteData(
            table: "DeviceTypes",
            keyColumn: "Id",
            keyValue: 4);
        migrationBuilder.DeleteData(
            table: "DeviceTypes",
            keyColumn: "Id",
            keyValue: 5);
        migrationBuilder.DeleteData(
            table: "DeviceTypes",
            keyColumn: "Id",
            keyValue: 6);
        migrationBuilder.DeleteData(
            table: "DeviceTypes",
            keyColumn: "Id",
            keyValue: 7);
        migrationBuilder.DeleteData(
            table: "DeviceTypes",
            keyColumn: "Id",
            keyValue: 8);
        migrationBuilder.DeleteData(
            table: "DeviceTypes",
            keyColumn: "Id",
            keyValue: 9);
        migrationBuilder.DeleteData(
            table: "DeviceTypes",
            keyColumn: "Id",
            keyValue: 10);
    }
}
