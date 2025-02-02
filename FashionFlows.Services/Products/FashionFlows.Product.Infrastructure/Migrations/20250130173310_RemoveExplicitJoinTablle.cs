using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFlows.Product.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExplicitJoinTablle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Category_CategoryId",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Product_ProductId",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ProductCategory",
                newName: "CategoriesId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductCategory",
                newName: "ProductsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory",
                columns: new[] { "CategoriesId", "ProductsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductsId",
                table: "ProductCategory",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Category_CategoriesId",
                table: "ProductCategory",
                column: "CategoriesId",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Product_ProductsId",
                table: "ProductCategory",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Category_CategoriesId",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Product_ProductsId",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_ProductsId",
                table: "ProductCategory");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "ProductCategory",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "ProductCategory",
                newName: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Category_CategoryId",
                table: "ProductCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Product_ProductId",
                table: "ProductCategory",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
