using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBallance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Ballance",
                table: "Users",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ballance",
                table: "Users");
        }
    }
}
