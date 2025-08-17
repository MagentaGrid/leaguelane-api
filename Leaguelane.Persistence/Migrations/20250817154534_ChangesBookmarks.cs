using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leaguelane.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangesBookmarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AffiliateLink",
                table: "Bookmakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApiBookMakerId",
                table: "Bookmakers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookieLogo",
                table: "Bookmakers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffiliateLink",
                table: "Bookmakers");

            migrationBuilder.DropColumn(
                name: "ApiBookMakerId",
                table: "Bookmakers");

            migrationBuilder.DropColumn(
                name: "BookieLogo",
                table: "Bookmakers");
        }
    }
}
