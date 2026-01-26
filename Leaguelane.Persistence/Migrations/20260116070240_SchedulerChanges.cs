using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leaguelane.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SchedulerChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApiLeagueSeasonId",
                table: "LeagueSeasons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ApiFixtureId",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApiBookMakerId",
                table: "Bookmakers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venues_ApiVenueId",
                table: "Venues",
                column: "ApiVenueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ApiTeamId",
                table: "Teams",
                column: "ApiTeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeagueSeasons_ApiLeagueSeasonId",
                table: "LeagueSeasons",
                column: "ApiLeagueSeasonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_ApiLeagueId",
                table: "Leagues",
                column: "ApiLeagueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_ApiFixtureId",
                table: "Fixtures",
                column: "ApiFixtureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmakers_ApiBookMakerId",
                table: "Bookmakers",
                column: "ApiBookMakerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bets_BetId",
                table: "Bets",
                column: "BetId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Venues_ApiVenueId",
                table: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ApiTeamId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_LeagueSeasons_ApiLeagueSeasonId",
                table: "LeagueSeasons");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_ApiLeagueId",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_ApiFixtureId",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Bookmakers_ApiBookMakerId",
                table: "Bookmakers");

            migrationBuilder.DropIndex(
                name: "IX_Bets_BetId",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "ApiLeagueSeasonId",
                table: "LeagueSeasons");

            migrationBuilder.AlterColumn<int>(
                name: "ApiFixtureId",
                table: "Fixtures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ApiBookMakerId",
                table: "Bookmakers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
