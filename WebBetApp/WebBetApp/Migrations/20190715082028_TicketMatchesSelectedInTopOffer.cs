using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBetApp.Migrations
{
    public partial class TicketMatchesSelectedInTopOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SelectedInTopOffer",
                table: "TicketMatches",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedInTopOffer",
                table: "TicketMatches");
        }
    }
}
