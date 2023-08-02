using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolGoharSales.Migrations
{
    /// <inheritdoc />
    public partial class ContextDbsets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Customer_CustomerId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Production_ProductionId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Production_Warehouses_WarehouseId",
                table: "Production");

            migrationBuilder.DropForeignKey(
                name: "FK_Transportation_Contract_ContractId",
                table: "Transportation");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Location_LocationId",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transportation",
                table: "Transportation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production",
                table: "Production");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                table: "Contract");

            migrationBuilder.RenameTable(
                name: "Transportation",
                newName: "Transportations");

            migrationBuilder.RenameTable(
                name: "Production",
                newName: "Productions");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Contract",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_Transportation_ContractId",
                table: "Transportations",
                newName: "IX_Transportations_ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Production_WarehouseId",
                table: "Productions",
                newName: "IX_Productions_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_ProductionId",
                table: "Contracts",
                newName: "IX_Contracts_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_CustomerId",
                table: "Contracts",
                newName: "IX_Contracts_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transportations",
                table: "Transportations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productions",
                table: "Productions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                table: "Contracts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Productions_ProductionId",
                table: "Contracts",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Warehouses_WarehouseId",
                table: "Productions",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Contracts_ContractId",
                table: "Transportations",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Locations_LocationId",
                table: "Warehouses",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Productions_ProductionId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Warehouses_WarehouseId",
                table: "Productions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Contracts_ContractId",
                table: "Transportations");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Locations_LocationId",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transportations",
                table: "Transportations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productions",
                table: "Productions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Transportations",
                newName: "Transportation");

            migrationBuilder.RenameTable(
                name: "Productions",
                newName: "Production");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contract");

            migrationBuilder.RenameIndex(
                name: "IX_Transportations_ContractId",
                table: "Transportation",
                newName: "IX_Transportation_ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Productions_WarehouseId",
                table: "Production",
                newName: "IX_Production_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_ProductionId",
                table: "Contract",
                newName: "IX_Contract_ProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CustomerId",
                table: "Contract",
                newName: "IX_Contract_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transportation",
                table: "Transportation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production",
                table: "Production",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Customer_CustomerId",
                table: "Contract",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Production_ProductionId",
                table: "Contract",
                column: "ProductionId",
                principalTable: "Production",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Production_Warehouses_WarehouseId",
                table: "Production",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transportation_Contract_ContractId",
                table: "Transportation",
                column: "ContractId",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Location_LocationId",
                table: "Warehouses",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
