using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leaguelane.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInOddValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Odd",
                table: "OddsValues",
                type: "text",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Odd",
                table: "OddsValues",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
