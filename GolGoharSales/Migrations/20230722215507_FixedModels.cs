using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolGoharSales.Migrations
{
    /// <inheritdoc />
    public partial class FixedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Transportations");

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractDate",
                table: "SalesContracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "SalesContracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "SalesContracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractDate",
                table: "SalesContracts");

            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "SalesContracts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "SalesContracts");

            migrationBuilder.AddColumn<int>(
                name: "ContractId",
                table: "Transportations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
