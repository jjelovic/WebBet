using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBetApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pair = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type1 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    TypeX = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Type2 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Type1X = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    TypeX2 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Type12 = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    IsPartOfTopOffer = table.Column<bool>(nullable: false),
                    SportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_SportId",
                table: "Matches",
                column: "SportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Sports");
        }
    }
}
