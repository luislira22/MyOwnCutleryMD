using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterDataFactory.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_machine_MachineTypes_MachineTypeid",
                table: "machine");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineTypes_MachineTypeID_Refid",
                table: "MachineTypes");

            migrationBuilder.DropTable(
                name: "MachineTypeID");

            migrationBuilder.DropIndex(
                name: "IX_MachineTypes_Refid",
                table: "MachineTypes");

            migrationBuilder.DropColumn(
                name: "Refid",
                table: "MachineTypes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MachineTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MachineTypeid",
                table: "machine",
                newName: "MachineTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_machine_MachineTypeid",
                table: "machine",
                newName: "IX_machine_MachineTypeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "MachineTypes",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "MachineTypeId",
                table: "machine",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_machine_MachineTypes_MachineTypeId",
                table: "machine",
                column: "MachineTypeId",
                principalTable: "MachineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_machine_MachineTypes_MachineTypeId",
                table: "machine");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MachineTypes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "MachineTypeId",
                table: "machine",
                newName: "MachineTypeid");

            migrationBuilder.RenameIndex(
                name: "IX_machine_MachineTypeId",
                table: "machine",
                newName: "IX_machine_MachineTypeid");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "MachineTypes",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Refid",
                table: "MachineTypes",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MachineTypeid",
                table: "machine",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MachineTypeID",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineTypeID", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineTypes_Refid",
                table: "MachineTypes",
                column: "Refid");

            migrationBuilder.AddForeignKey(
                name: "FK_machine_MachineTypes_MachineTypeid",
                table: "machine",
                column: "MachineTypeid",
                principalTable: "MachineTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineTypes_MachineTypeID_Refid",
                table: "MachineTypes",
                column: "Refid",
                principalTable: "MachineTypeID",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
