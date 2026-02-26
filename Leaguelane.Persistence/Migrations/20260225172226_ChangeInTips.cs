using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leaguelane.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInTips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixtureTips_Odds_OddsId",
                table: "FixtureTips");

            migrationBuilder.RenameColumn(
                name: "OddsId",
                table: "FixtureTips",
                newName: "OddsValueId");

            migrationBuilder.RenameIndex(
                name: "IX_FixtureTips_OddsId",
                table: "FixtureTips",
                newName: "IX_FixtureTips_OddsValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixtureTips_OddsValues_OddsValueId",
                table: "FixtureTips",
                column: "OddsValueId",
                principalTable: "OddsValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FixtureTips_OddsValues_OddsValueId",
                table: "FixtureTips");

            migrationBuilder.RenameColumn(
                name: "OddsValueId",
                table: "FixtureTips",
                newName: "OddsId");

            migrationBuilder.RenameIndex(
                name: "IX_FixtureTips_OddsValueId",
                table: "FixtureTips",
                newName: "IX_FixtureTips_OddsId");

            migrationBuilder.AddForeignKey(
                name: "FK_FixtureTips_Odds_OddsId",
                table: "FixtureTips",
                column: "OddsId",
                principalTable: "Odds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
