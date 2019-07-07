using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBetApp.Migrations
{
    public partial class TicketCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMatchesCoefficinet",
                table: "Tickets");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMatchesCoefficient",
                table: "Tickets",
                type: "decimal(19,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMatchesCoefficient",
                table: "Tickets");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMatchesCoefficinet",
                table: "Tickets",
                type: "decimal(19,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
