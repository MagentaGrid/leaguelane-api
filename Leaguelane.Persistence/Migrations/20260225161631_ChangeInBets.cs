using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leaguelane.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInBets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bets_BetId",
                table: "Bets");

            migrationBuilder.AddColumn<int>(
                name: "ApiBetId",
                table: "Bets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bets_ApiBetId",
                table: "Bets",
                column: "ApiBetId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bets_ApiBetId",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "ApiBetId",
                table: "Bets");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_BetId",
                table: "Bets",
                column: "BetId",
                unique: true);
        }
    }
}
