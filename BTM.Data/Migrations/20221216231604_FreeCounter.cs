using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTM.Data.Migrations
{
    /// <inheritdoc />
    public partial class FreeCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FreePrintsBlackWhite",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreePrintsColor",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreePrintsBlackWhite",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "FreePrintsColor",
                table: "Devices");
        }
    }
}
