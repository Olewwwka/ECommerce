using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Categories_CategoryId1",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_CategoryId1",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Attributes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "Attributes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CategoryId1",
                table: "Attributes",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Categories_CategoryId1",
                table: "Attributes",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
