using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestKSK.Migrations
{
    /// <inheritdoc />
    public partial class AddCountToBeverage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Count",
                table: "Beverage",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Beverage");
        }
    }
}
