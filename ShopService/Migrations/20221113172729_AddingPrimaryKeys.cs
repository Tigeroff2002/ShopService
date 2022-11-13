using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopService.Migrations
{
    /// <inheritdoc />
    public partial class AddingPrimaryKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Clients_ClientId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Basket_Id",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProduct_Basket_BasketStatusId",
                table: "SummUpProduct");

            migrationBuilder.DropIndex(
                name: "IX_SummUpProduct_BasketStatusId",
                table: "SummUpProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Basket",
                table: "Basket");

            migrationBuilder.AddColumn<int>(
                name: "BasketClientId",
                table: "SummUpProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

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

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Basket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BasketStatusId",
                table: "Basket",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basket",
                table: "Basket",
                columns: new[] { "BasketStatusId", "ClientId" });

            migrationBuilder.CreateIndex(
                name: "IX_SummUpProduct_BasketStatusId_BasketClientId",
                table: "SummUpProduct",
                columns: new[] { "BasketStatusId", "BasketClientId" });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BasketStatusId_BasketClientId",
                table: "Clients",
                columns: new[] { "BasketStatusId", "BasketClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Clients_ClientId",
                table: "Basket",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Basket_BasketStatusId_BasketClientId",
                table: "Clients",
                columns: new[] { "BasketStatusId", "BasketClientId" },
                principalTable: "Basket",
                principalColumns: new[] { "BasketStatusId", "ClientId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProduct_Basket_BasketStatusId_BasketClientId",
                table: "SummUpProduct",
                columns: new[] { "BasketStatusId", "BasketClientId" },
                principalTable: "Basket",
                principalColumns: new[] { "BasketStatusId", "ClientId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Clients_ClientId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Basket_BasketStatusId_BasketClientId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProduct_Basket_BasketStatusId_BasketClientId",
                table: "SummUpProduct");

            migrationBuilder.DropIndex(
                name: "IX_SummUpProduct_BasketStatusId_BasketClientId",
                table: "SummUpProduct");

            migrationBuilder.DropIndex(
                name: "IX_Clients_BasketStatusId_BasketClientId",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Basket",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "BasketClientId",
                table: "SummUpProduct");

            migrationBuilder.DropColumn(
                name: "BasketClientId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "BasketStatusId",
                table: "Clients");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Basket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.CreateIndex(
                name: "IX_SummUpProduct_BasketStatusId",
                table: "SummUpProduct",
                column: "BasketStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Clients_ClientId",
                table: "Basket",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

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
    }
}
