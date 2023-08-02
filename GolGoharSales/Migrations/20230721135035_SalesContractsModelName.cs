using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolGoharSales.Migrations
{
    /// <inheritdoc />
    public partial class SalesContractsModelName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Contracts_ContractId",
                table: "Transportations");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_ContractId",
                table: "Transportations");

            migrationBuilder.AddColumn<int>(
                name: "SalesContractId",
                table: "Transportations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SalesContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractNumber = table.Column<int>(type: "int", nullable: false),
                    Monthly = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ProductionId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesContracts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesContracts_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_SalesContractId",
                table: "Transportations",
                column: "SalesContractId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesContracts_CustomerId",
                table: "SalesContracts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesContracts_ProductionId",
                table: "SalesContracts",
                column: "ProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_SalesContracts_SalesContractId",
                table: "Transportations",
                column: "SalesContractId",
                principalTable: "SalesContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_SalesContracts_SalesContractId",
                table: "Transportations");

            migrationBuilder.DropTable(
                name: "SalesContracts");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_SalesContractId",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "SalesContractId",
                table: "Transportations");

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductionId = table.Column<int>(type: "int", nullable: false),
                    ContractNumber = table.Column<int>(type: "int", nullable: false),
                    Monthly = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_ContractId",
                table: "Transportations",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerId",
                table: "Contracts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ProductionId",
                table: "Contracts",
                column: "ProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Contracts_ContractId",
                table: "Transportations",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
