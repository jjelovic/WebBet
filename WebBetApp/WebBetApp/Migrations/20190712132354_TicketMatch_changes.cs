using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBetApp.Migrations
{
    public partial class TicketMatch_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketMatches_Tickets_TicketId",
                table: "TicketMatches");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "TicketMatches",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "TicketMatches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMatches_Tickets_TicketId",
                table: "TicketMatches",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketMatches_Tickets_TicketId",
                table: "TicketMatches");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "TicketMatches");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "TicketMatches",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketMatches_Tickets_TicketId",
                table: "TicketMatches",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
