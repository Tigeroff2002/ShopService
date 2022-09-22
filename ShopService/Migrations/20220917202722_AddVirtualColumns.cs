using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ShopService.Models;
#nullable disable

namespace ShopService.Migrations
{
    /// <inheritdoc />
    public partial class AddVirtualColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableCount",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "Ram",
                table: "Devices",
                newName: "RAM");

            migrationBuilder.AlterColumn<string>(
                name: "Adress",
                table: "Warehouses",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WebSite",
                table: "Producer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "DeviceTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TelephoneNumber",
                table: "Clients",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trades_ClientId",
                table: "Trades",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_DeviceId",
                table: "Trades",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_WareHouseId",
                table: "Trades",
                column: "WareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ProducerId",
                table: "Devices",
                column: "ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Producer_ProducerId",
                table: "Devices",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_Clients_ClientId",
                table: "Trades",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_Devices_DeviceId",
                table: "Trades",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_Warehouses_WareHouseId",
                table: "Trades",
                column: "WareHouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Producer_ProducerId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Trades_Clients_ClientId",
                table: "Trades");

            migrationBuilder.DropForeignKey(
                name: "FK_Trades_Devices_DeviceId",
                table: "Trades");

            migrationBuilder.DropForeignKey(
                name: "FK_Trades_Warehouses_WareHouseId",
                table: "Trades");

            migrationBuilder.DropIndex(
                name: "IX_Trades_ClientId",
                table: "Trades");

            migrationBuilder.DropIndex(
                name: "IX_Trades_DeviceId",
                table: "Trades");

            migrationBuilder.DropIndex(
                name: "IX_Trades_WareHouseId",
                table: "Trades");

            migrationBuilder.DropIndex(
                name: "IX_Devices_DeviceTypeId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_ProducerId",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "RAM",
                table: "Devices",
                newName: "Ram");

            migrationBuilder.AlterColumn<string>(
                name: "Adress",
                table: "Warehouses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WebSite",
                table: "Producer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "DeviceTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvailableCount",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Devices",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "TelephoneNumber",
                table: "Clients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);
        }
    }
}
