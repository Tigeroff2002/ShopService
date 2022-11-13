using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopService.Migrations
{
    /// <inheritdoc />
    public partial class DeletingBasketId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Basket_Id",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProduct_Basket_BasketId",
                table: "SummUpProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Basket",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "BasketId",
                table: "SummUpProduct",
                newName: "BasketStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_SummUpProduct_BasketId",
                table: "SummUpProduct",
                newName: "IX_SummUpProduct_BasketStatusId");

            migrationBuilder.AlterColumn<int>(
                name: "BasketStatusId",
                table: "Basket",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basket",
                table: "Basket",
                column: "BasketStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Basket_Id",
                table: "Clients",
                column: "Id",
                principalTable: "Basket",
                principalColumn: "BasketStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProduct_Basket_BasketStatusId",
                table: "SummUpProduct",
                column: "BasketStatusId",
                principalTable: "Basket",
                principalColumn: "BasketStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Basket_Id",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProduct_Basket_BasketStatusId",
                table: "SummUpProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Basket",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "BasketStatusId",
                table: "SummUpProduct",
                newName: "BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_SummUpProduct_BasketStatusId",
                table: "SummUpProduct",
                newName: "IX_SummUpProduct_BasketId");

            migrationBuilder.AlterColumn<int>(
                name: "BasketStatusId",
                table: "Basket",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Basket",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basket",
                table: "Basket",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Basket_Id",
                table: "Clients",
                column: "Id",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProduct_Basket_BasketId",
                table: "SummUpProduct",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id");
        }
    }
}
