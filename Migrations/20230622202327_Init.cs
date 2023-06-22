using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TestKSK.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendingMachine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserBalance = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendingMachine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beverage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    VendingMachineId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beverage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beverage_VendingMachine_VendingMachineId",
                        column: x => x.VendingMachineId,
                        principalTable: "VendingMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MoneyUnit",
                columns: table => new
                {
                    Denomination = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    VendingMachineId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyUnit", x => x.Denomination);
                    table.ForeignKey(
                        name: "FK_MoneyUnit_VendingMachine_VendingMachineId",
                        column: x => x.VendingMachineId,
                        principalTable: "VendingMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beverage_VendingMachineId",
                table: "Beverage",
                column: "VendingMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyUnit_VendingMachineId",
                table: "MoneyUnit",
                column: "VendingMachineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beverage");

            migrationBuilder.DropTable(
                name: "MoneyUnit");

            migrationBuilder.DropTable(
                name: "VendingMachine");
        }
    }
}
