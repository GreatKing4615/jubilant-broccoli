using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JubilantBroccoli.Infrastructure.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddItemTypetoItemOptionsandaddnewitemtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "DeliveryTime",
                table: "Orders",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ItemOptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ItemOptions");

            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "Orders");
        }
    }
}
