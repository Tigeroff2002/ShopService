using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class BasketKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProducts_Baskets_BasketStatusId_BasketClientId",
                table: "SummUpProducts");

            migrationBuilder.DropIndex(
                name: "IX_SummUpProducts_BasketStatusId_BasketClientId",
                table: "SummUpProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "BasketClientId",
                table: "SummUpProducts");

            migrationBuilder.RenameColumn(
                name: "BasketStatusId",
                table: "SummUpProducts",
                newName: "BasketId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SummUpProducts_BasketId",
                table: "SummUpProducts",
                column: "BasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProducts_Baskets_BasketId",
                table: "SummUpProducts",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SummUpProducts_Baskets_BasketId",
                table: "SummUpProducts");

            migrationBuilder.DropIndex(
                name: "IX_SummUpProducts_BasketId",
                table: "SummUpProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "BasketId",
                table: "SummUpProducts",
                newName: "BasketStatusId");

            migrationBuilder.AddColumn<int>(
                name: "BasketClientId",
                table: "SummUpProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets",
                columns: new[] { "BasketStatusId", "ClientId" });

            migrationBuilder.CreateIndex(
                name: "IX_SummUpProducts_BasketStatusId_BasketClientId",
                table: "SummUpProducts",
                columns: new[] { "BasketStatusId", "BasketClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SummUpProducts_Baskets_BasketStatusId_BasketClientId",
                table: "SummUpProducts",
                columns: new[] { "BasketStatusId", "BasketClientId" },
                principalTable: "Baskets",
                principalColumns: new[] { "BasketStatusId", "ClientId" });
        }
    }
}
