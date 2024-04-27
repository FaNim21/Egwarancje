using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgwarancjeDbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class warrantycascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarrantiesSpecs_Warranties_WarrantyId",
                table: "WarrantiesSpecs");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantiesSpecs_Warranties_WarrantyId",
                table: "WarrantiesSpecs",
                column: "WarrantyId",
                principalTable: "Warranties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarrantiesSpecs_Warranties_WarrantyId",
                table: "WarrantiesSpecs");

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantiesSpecs_Warranties_WarrantyId",
                table: "WarrantiesSpecs",
                column: "WarrantyId",
                principalTable: "Warranties",
                principalColumn: "Id");
        }
    }
}
