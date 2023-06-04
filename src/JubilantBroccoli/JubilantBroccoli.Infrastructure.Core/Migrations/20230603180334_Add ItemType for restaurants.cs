using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JubilantBroccoli.Infrastructure.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddItemTypeforrestaurants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<int>>(
                name: "ItemTypes",
                table: "Restaurants",
                type: "integer[]",
                maxLength: 300,
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemTypes",
                table: "Restaurants");
        }
    }
}
