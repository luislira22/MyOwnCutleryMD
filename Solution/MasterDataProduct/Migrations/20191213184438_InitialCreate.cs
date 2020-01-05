using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterDataProduct.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Reference_Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturingPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturingPlan_Product_Id",
                        column: x => x.Id,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationId",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    ManufacturingPlanId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationId_ManufacturingPlan_ManufacturingPlanId",
                        column: x => x.ManufacturingPlanId,
                        principalTable: "ManufacturingPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationId_ManufacturingPlanId",
                table: "OperationId",
                column: "ManufacturingPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationId");

            migrationBuilder.DropTable(
                name: "ManufacturingPlan");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
