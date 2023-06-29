using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TestKSK.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoneyUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MoneyUnit",
                table: "MoneyUnit");

            migrationBuilder.AlterColumn<long>(
                name: "Denomination",
                table: "MoneyUnit",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "MoneyUnit",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoneyUnit",
                table: "MoneyUnit",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MoneyUnit",
                table: "MoneyUnit");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MoneyUnit");

            migrationBuilder.AlterColumn<long>(
                name: "Denomination",
                table: "MoneyUnit",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoneyUnit",
                table: "MoneyUnit",
                column: "Denomination");
        }
    }
}
