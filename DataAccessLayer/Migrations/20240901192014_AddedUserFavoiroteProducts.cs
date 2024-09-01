using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserFavoiroteProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteProduct_AspNetUsers_UserId",
                table: "UserFavoriteProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteProduct_Products_ProductId",
                table: "UserFavoriteProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavoriteProduct",
                table: "UserFavoriteProduct");

            migrationBuilder.RenameTable(
                name: "UserFavoriteProduct",
                newName: "UserFavoriteProducts");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavoriteProduct_UserId",
                table: "UserFavoriteProducts",
                newName: "IX_UserFavoriteProducts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavoriteProducts",
                table: "UserFavoriteProducts",
                columns: new[] { "ProductId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteProducts_AspNetUsers_UserId",
                table: "UserFavoriteProducts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteProducts_Products_ProductId",
                table: "UserFavoriteProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteProducts_AspNetUsers_UserId",
                table: "UserFavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavoriteProducts_Products_ProductId",
                table: "UserFavoriteProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavoriteProducts",
                table: "UserFavoriteProducts");

            migrationBuilder.RenameTable(
                name: "UserFavoriteProducts",
                newName: "UserFavoriteProduct");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavoriteProducts_UserId",
                table: "UserFavoriteProduct",
                newName: "IX_UserFavoriteProduct_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavoriteProduct",
                table: "UserFavoriteProduct",
                columns: new[] { "ProductId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteProduct_AspNetUsers_UserId",
                table: "UserFavoriteProduct",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavoriteProduct_Products_ProductId",
                table: "UserFavoriteProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
