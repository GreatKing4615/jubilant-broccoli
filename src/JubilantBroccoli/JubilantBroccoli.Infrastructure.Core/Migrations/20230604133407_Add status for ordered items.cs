using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JubilantBroccoli.Infrastructure.Core.Migrations
{
    /// <inheritdoc />
    public partial class Addstatusforordereditems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "OrderedItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrderedItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderedItems");
        }
    }
}
