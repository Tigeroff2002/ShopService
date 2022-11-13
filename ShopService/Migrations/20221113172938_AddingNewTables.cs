using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopService.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Basket_BasketStatusId_BasketClientId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Clients_RecipientId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProduct_Basket_BasketStatusId_BasketClientId",
                table: "SummUpProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProduct_Products_ProductId",
                table: "SummUpProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProduct_Warehouses_WarehouseId",
                table: "SummUpProduct");

            migrationBuilder.DropIndex(
                name: "IX_Clients_BasketStatusId_BasketClientId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Basket_ClientId",
                table: "Basket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SummUpProduct",
                table: "SummUpProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "BasketClientId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "BasketStatusId",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "SummUpProduct",
                newName: "SummUpProducts");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_SummUpProduct_WarehouseId",
                table: "SummUpProducts",
                newName: "IX_SummUpProducts_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_SummUpProduct_ProductId",
                table: "SummUpProducts",
                newName: "IX_SummUpProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SummUpProduct_BasketStatusId_BasketClientId",
                table: "SummUpProducts",
                newName: "IX_SummUpProducts_BasketStatusId_BasketClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_RecipientId",
                table: "Notifications",
                newName: "IX_Notifications_RecipientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SummUpProducts",
                table: "SummUpProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_ClientId",
                table: "Basket",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Clients_RecipientId",
                table: "Notifications",
                column: "RecipientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProducts_Basket_BasketStatusId_BasketClientId",
                table: "SummUpProducts",
                columns: new[] { "BasketStatusId", "BasketClientId" },
                principalTable: "Basket",
                principalColumns: new[] { "BasketStatusId", "ClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProducts_Products_ProductId",
                table: "SummUpProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProducts_Warehouses_WarehouseId",
                table: "SummUpProducts",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Clients_RecipientId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProducts_Basket_BasketStatusId_BasketClientId",
                table: "SummUpProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProducts_Products_ProductId",
                table: "SummUpProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProducts_Warehouses_WarehouseId",
                table: "SummUpProducts");

            migrationBuilder.DropIndex(
                name: "IX_Basket_ClientId",
                table: "Basket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SummUpProducts",
                table: "SummUpProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "SummUpProducts",
                newName: "SummUpProduct");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameIndex(
                name: "IX_SummUpProducts_WarehouseId",
                table: "SummUpProduct",
                newName: "IX_SummUpProduct_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_SummUpProducts_ProductId",
                table: "SummUpProduct",
                newName: "IX_SummUpProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SummUpProducts_BasketStatusId_BasketClientId",
                table: "SummUpProduct",
                newName: "IX_SummUpProduct_BasketStatusId_BasketClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_RecipientId",
                table: "Notification",
                newName: "IX_Notification_RecipientId");

            migrationBuilder.AddColumn<int>(
                name: "BasketClientId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BasketStatusId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SummUpProduct",
                table: "SummUpProduct",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BasketStatusId_BasketClientId",
                table: "Clients",
                columns: new[] { "BasketStatusId", "BasketClientId" });

            migrationBuilder.CreateIndex(
                name: "IX_Basket_ClientId",
                table: "Basket",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Basket_BasketStatusId_BasketClientId",
                table: "Clients",
                columns: new[] { "BasketStatusId", "BasketClientId" },
                principalTable: "Basket",
                principalColumns: new[] { "BasketStatusId", "ClientId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Clients_RecipientId",
                table: "Notification",
                column: "RecipientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProduct_Basket_BasketStatusId_BasketClientId",
                table: "SummUpProduct",
                columns: new[] { "BasketStatusId", "BasketClientId" },
                principalTable: "Basket",
                principalColumns: new[] { "BasketStatusId", "ClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProduct_Products_ProductId",
                table: "SummUpProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProduct_Warehouses_WarehouseId",
                table: "SummUpProduct",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }
    }
}
