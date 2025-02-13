using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFlows.Payment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addNewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CheckOutSessionId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckOutSessionId",
                table: "Payments");
        }
    }
}
