using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguringConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "SummUpProducts");

            migrationBuilder.CreateTable(
               name: "SummUpProducts",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   ProductId = table.Column<int>(type: "int", nullable: false),
                   Quantity = table.Column<int>(type: "int", nullable: false),
                   TotalPrice = table.Column<float>(type: "real", nullable: true),
                   BasketId = table.Column<int>(type: "int", nullable: true),
                   OrderId = table.Column<int>(type: "int", nullable: true),
                   ShopId = table.Column<int>(type: "int", nullable: true),
                   WarehouseId = table.Column<int>(type: "int", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_SummUpProducts", x => x.Id);
                   table.ForeignKey(
                       name: "FK_SummUpProducts_Baskets_BasketId",
                       column: x => x.BasketId,
                       principalTable: "Baskets",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_SummUpProducts_Orders_OrderId",
                       column: x => x.OrderId,
                       principalTable: "Orders",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_SummUpProducts_Products_ProductId",
                       column: x => x.ProductId,
                       principalTable: "Products",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_SummUpProducts_Shops_ShopId",
                       column: x => x.ShopId,
                       principalTable: "Shops",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_SummUpProducts_Warehouses_WarehouseId",
                       column: x => x.WarehouseId,
                       principalTable: "Warehouses",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.DropIndex(name: "IX_Baskets_ClientId", table: "Baskets");

            migrationBuilder.CreateIndex(
                 name: "IX_Baskets_ClientId",
                 table: "Baskets",
                column: "ClientId",
                unique: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
