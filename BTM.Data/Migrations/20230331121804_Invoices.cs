using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTM.Data.Migrations
{
    /// <inheritdoc />
    public partial class Invoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InvoiceIsCreated",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceIsCreated",
                table: "Customers");
        }
    }
}
