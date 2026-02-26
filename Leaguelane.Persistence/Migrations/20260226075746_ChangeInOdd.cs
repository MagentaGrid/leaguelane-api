using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leaguelane.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInOdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OddsValues_Odds_OddId",
                table: "OddsValues");

            migrationBuilder.DropIndex(
                name: "IX_OddsValues_OddId",
                table: "OddsValues");

            migrationBuilder.DropColumn(
                name: "OddId",
                table: "OddsValues");

            migrationBuilder.CreateIndex(
                name: "IX_OddsValues_OddsId",
                table: "OddsValues",
                column: "OddsId");

            migrationBuilder.AddForeignKey(
                name: "FK_OddsValues_Odds_OddsId",
                table: "OddsValues",
                column: "OddsId",
                principalTable: "Odds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OddsValues_Odds_OddsId",
                table: "OddsValues");

            migrationBuilder.DropIndex(
                name: "IX_OddsValues_OddsId",
                table: "OddsValues");

            migrationBuilder.AddColumn<int>(
                name: "OddId",
                table: "OddsValues",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OddsValues_OddId",
                table: "OddsValues",
                column: "OddId");

            migrationBuilder.AddForeignKey(
                name: "FK_OddsValues_Odds_OddId",
                table: "OddsValues",
                column: "OddId",
                principalTable: "Odds",
                principalColumn: "Id");
        }
    }
}
