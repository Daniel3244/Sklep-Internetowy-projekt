using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sklep_internetowy_projekt.Migrations
{
    /// <inheritdoc />
    public partial class InitialCerate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b91a8b13-2430-4f6f-bf0b-fdf91a9eea1e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c77d2e7c-f68d-4f82-9fdf-41a2aa24616d");

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ShoppingCartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a65f731-c12d-4efe-a76e-f5f036849e24", "84a1008a-2196-46dd-9078-45a0a9529f92", "Admin", "ADMIN" },
                    { "42594147-9ca0-4174-8785-014c53b29169", "c9bfbb63-c5a1-4165-bc97-de5e55cb480e", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItem_ProductId1",
                table: "ShoppingCartItem",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItem_Products_ProductId1",
                table: "ShoppingCartItem",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItem_Products_ProductId1",
                table: "ShoppingCartItem");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItem_ProductId1",
                table: "ShoppingCartItem");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a65f731-c12d-4efe-a76e-f5f036849e24");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42594147-9ca0-4174-8785-014c53b29169");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ShoppingCartItem");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b91a8b13-2430-4f6f-bf0b-fdf91a9eea1e", "796ce952-0778-4c4f-89c1-e31c17494038", "Admin", "ADMIN" },
                    { "c77d2e7c-f68d-4f82-9fdf-41a2aa24616d", "4d391d7b-b632-43ae-95ab-1e9cdb299a36", "User", "USER" }
                });
        }
    }
}
