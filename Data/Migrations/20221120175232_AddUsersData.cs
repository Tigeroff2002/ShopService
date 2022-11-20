using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
