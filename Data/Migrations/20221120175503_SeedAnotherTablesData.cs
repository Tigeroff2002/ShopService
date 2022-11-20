using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAnotherTablesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Caption", "RoleId" },
                values: new object[] { 1, "View catalog", 1 });
            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Caption", "RoleId" },
                values: new object[] { 2, "Add Product To Basket", 1 });
            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Caption", "RoleId" },
                values: new object[] { 3, "Make Order", 1 });
            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Caption", "RoleId" },
                values: new object[] { 4, "Write review", 1 });
            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Caption", "RoleId" },
                values: new object[] { 5, "Product's management", 2 });
            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Caption", "RoleId" },
                values: new object[] { 6, "Order shippings", 2 });
            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Caption", "RoleId" },
                values: new object[] { 7, "Confirm getting order", 3 });
            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Caption", "RoleId" },
                values: new object[] { 8, "Moderate reviews", 3 });
            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Caption", "RoleId" },
                values: new object[] { 9, "Change state of frontend block", 3 });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name", "Address", "WorkingTime" },
                values: new object[] { 1, "Главный магазин сети", "г.Владимир, ул. Добросельская, д.176", "9:00-20:00" });
            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name", "Address", "WorkingTime" },
                values: new object[] { 2, "Дополнительный магазин сети (дублер)", "г.Владимир, ул. Горького, д.16", "10:00-20:00" });
            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name", "Address", "WorkingTime" },
                values: new object[] { 3, "Мини-магазин", "г.Владимир, ул. Верхняя Дуброва, д.40", "9:00-21:00" });
            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name", "Address", "WorkingTime" },
                values: new object[] { 4, "Магазин-Ковров", "г.Ковров, ул. Ленина, д.126", "10:00-21:00" });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Основной склад г.Владимир" });
            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Доп. склад г.Владимир" });
            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Склад г.Ковров" });

            migrationBuilder.InsertData(
                table: "SummUpProducts",
                columns: new[] { "Id", "ProductId", "Quantity" },
                values: new object[] { 1, 1, 10 });
            migrationBuilder.InsertData(
                table: "SummUpProducts",
                columns: new[] { "Id", "ProductId", "Quantity" },
                values: new object[] { 2, 1, 30 });
            migrationBuilder.InsertData(
                table: "SummUpProducts",
                columns: new[] { "Id", "ProductId", "Quantity" },
                values: new object[] { 3, 2, 15 });
            migrationBuilder.InsertData(
                table: "SummUpProducts",
                columns: new[] { "Id", "ProductId", "Quantity" },
                values: new object[] { 4, 3, 20 });
            migrationBuilder.InsertData(
                table: "SummUpProducts",
                columns: new[] { "Id", "ProductId", "Quantity" },
                values: new object[] { 5, 4, 10 });
            migrationBuilder.InsertData(
                table: "SummUpProducts",
                columns: new[] { "Id", "ProductId", "Quantity" },
                values: new object[] { 6, 4, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                            table: "Options",
                            keyColumn: "Id",
                            keyValue: 1);
            migrationBuilder.DeleteData(
                           table: "Options",
                           keyColumn: "Id",
                           keyValue: 2);
            migrationBuilder.DeleteData(
                           table: "Options",
                           keyColumn: "Id",
                           keyValue: 3);
            migrationBuilder.DeleteData(
                           table: "Options",
                           keyColumn: "Id",
                           keyValue: 4);
            migrationBuilder.DeleteData(
                           table: "Options",
                           keyColumn: "Id",
                           keyValue: 5);
            migrationBuilder.DeleteData(
                           table: "Options",
                           keyColumn: "Id",
                           keyValue: 6);
            migrationBuilder.DeleteData(
                           table: "Options",
                           keyColumn: "Id",
                           keyValue: 7);
            migrationBuilder.DeleteData(
                           table: "Options",
                           keyColumn: "Id",
                           keyValue: 8);
            migrationBuilder.DeleteData(
                           table: "Options",
                           keyColumn: "Id",
                           keyValue: 9);

            migrationBuilder.DeleteData(
                           table: "Shops",
                           keyColumn: "Id",
                           keyValue: 1);
            migrationBuilder.DeleteData(
                           table: "Shops",
                           keyColumn: "Id",
                           keyValue: 2);
            migrationBuilder.DeleteData(
                           table: "Shops",
                           keyColumn: "Id",
                           keyValue: 3);
            migrationBuilder.DeleteData(
                           table: "Shops",
                           keyColumn: "Id",
                           keyValue: 4);

            migrationBuilder.DeleteData(
                           table: "Warehouses",
                           keyColumn: "Id",
                           keyValue: 1);
            migrationBuilder.DeleteData(
                           table: "Warehouses",
                           keyColumn: "Id",
                           keyValue: 2);
            migrationBuilder.DeleteData(
                           table: "Warehouses",
                           keyColumn: "Id",
                           keyValue: 3);

            migrationBuilder.DeleteData(
                           table: "SummUpProducts",
                           keyColumn: "Id",
                           keyValue: 1);
            migrationBuilder.DeleteData(
                           table: "SummUpProducts",
                           keyColumn: "Id",
                           keyValue: 2);
            migrationBuilder.DeleteData(
                           table: "SummUpProducts",
                           keyColumn: "Id",
                           keyValue: 3);
            migrationBuilder.DeleteData(
                           table: "SummUpProducts",
                           keyColumn: "Id",
                           keyValue: 4);
            migrationBuilder.DeleteData(
                           table: "SummUpProducts",
                           keyColumn: "Id",
                           keyValue: 5);
            migrationBuilder.DeleteData(
                           table: "SummUpProducts",
                           keyColumn: "Id",
                           keyValue: 6);
        }
    }
}
