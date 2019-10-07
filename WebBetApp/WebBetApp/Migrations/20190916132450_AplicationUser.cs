using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBetApp.Migrations
{
    public partial class AplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ApplicationUserId",
                table: "Transactions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ApplicationUserId",
                table: "Tickets",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ApplicationUsers_ApplicationUserId",
                table: "Tickets",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ApplicationUsers_ApplicationUserId",
                table: "Transactions",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ApplicationUsers_ApplicationUserId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ApplicationUsers_ApplicationUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ApplicationUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ApplicationUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tickets");
        }
    }
}
