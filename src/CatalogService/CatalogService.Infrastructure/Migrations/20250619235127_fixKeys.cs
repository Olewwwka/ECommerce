using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValue_Attributes_AttributeId",
                table: "ProductAttributeValue");

            migrationBuilder.RenameColumn(
                name: "AttributeId",
                table: "ProductAttributeValue",
                newName: "ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributeValue_AttributeId",
                table: "ProductAttributeValue",
                newName: "IX_ProductAttributeValue_ProductAttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValue_Attributes_ProductAttributeId",
                table: "ProductAttributeValue",
                column: "ProductAttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributeValue_Attributes_ProductAttributeId",
                table: "ProductAttributeValue");

            migrationBuilder.RenameColumn(
                name: "ProductAttributeId",
                table: "ProductAttributeValue",
                newName: "AttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributeValue_ProductAttributeId",
                table: "ProductAttributeValue",
                newName: "IX_ProductAttributeValue_AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributeValue_Attributes_AttributeId",
                table: "ProductAttributeValue",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
