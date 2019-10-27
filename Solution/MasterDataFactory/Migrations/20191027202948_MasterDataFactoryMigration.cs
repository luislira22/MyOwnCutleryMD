using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterDataFactory.Migrations
{
    public partial class MasterDataFactoryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Duration_Duration = table.Column<TimeSpan>(nullable: false),
                    Description_Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineTypeOperations",
                columns: table => new
                {
                    MachineTypeId = table.Column<Guid>(nullable: false),
                    OperationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineTypeOperations", x => new { x.MachineTypeId, x.OperationId });
                    table.ForeignKey(
                        name: "FK_MachineTypeOperations_MachineTypes_MachineTypeId",
                        column: x => x.MachineTypeId,
                        principalTable: "MachineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineTypeOperations_operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "machine",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MachineTypeId = table.Column<Guid>(nullable: true),
                    MachineBrand_Brand = table.Column<string>(nullable: true),
                    MachineModel_Model = table.Column<string>(nullable: true),
                    MachineLocation_Location = table.Column<string>(nullable: true),
                    ProductionLineId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_machine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_machine_MachineTypes_MachineTypeId",
                        column: x => x.MachineTypeId,
                        principalTable: "MachineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_machine_ProductionLines_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_machine_MachineTypeId",
                table: "machine",
                column: "MachineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_machine_ProductionLineId",
                table: "machine",
                column: "ProductionLineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineTypeOperations_OperationId",
                table: "MachineTypeOperations",
                column: "OperationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "machine");

            migrationBuilder.DropTable(
                name: "MachineTypeOperations");

            migrationBuilder.DropTable(
                name: "ProductionLines");

            migrationBuilder.DropTable(
                name: "MachineTypes");

            migrationBuilder.DropTable(
                name: "operations");
        }
    }
}
