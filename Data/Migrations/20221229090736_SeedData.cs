using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
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

            migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Id", "RoleType", "RoleCaption" },
            values: new object[] { 1, 1, "Authorized User" });
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType", "RoleCaption" },
                values: new object[] { 2, 2, "Manager" });
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType", "RoleCaption" },
                values: new object[] { 3, 3, "Administrator" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "NickName", "Password", "ContactNumber", "EmailAdress", "TotalPurchase", "Discount", "RoleId" },
                values: new object[] { 1, "Tigeroff", "tigeroff2002", "+79042555027", "parahinkv@gmail.com", 0f, 0f, 1 });
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "NickName", "Password", "ContactNumber", "EmailAdress", "TotalPurchase", "Discount", "RoleId" },
                values: new object[] { 2, "MLG", "tigeroff2002", "+79036485574", "parahinvs@gmail.com", 0f, 0f, 2 });
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "NickName", "Password", "ContactNumber", "EmailAdress", "TotalPurchase", "Discount", "RoleId" },
                values: new object[] { 3, "Tigeroff2002", "tigeroff2002", "+79046594765", "parahinnv@gmail.com", 0f, 0f, 3 });

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

            migrationBuilder.DeleteData(
                      table: "Roles",
                      keyColumn: "Id",
                      keyValue: 1);
            migrationBuilder.DeleteData(
                           table: "Roles",
                           keyColumn: "Id",
                           keyValue: 2);
            migrationBuilder.DeleteData(
                           table: "Roles",
                           keyColumn: "Id",
                           keyValue: 3);

            migrationBuilder.DeleteData(
                           table: "Clients",
                           keyColumn: "Id",
                           keyValue: 1);
            migrationBuilder.DeleteData(
                           table: "Clients",
                           keyColumn: "Id",
                           keyValue: 2);
            migrationBuilder.DeleteData(
                           table: "Clients",
                           keyColumn: "Id",
                           keyValue: 3);

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
