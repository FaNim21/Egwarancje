using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgwarancjeDbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class warrantytest1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Users_OrderId",
                table: "Warranties");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Warranties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_UserId",
                table: "Warranties",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warranties_Users_UserId",
                table: "Warranties",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Users_UserId",
                table: "Warranties");

            migrationBuilder.DropIndex(
                name: "IX_Warranties_UserId",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Warranties");

            migrationBuilder.AddForeignKey(
                name: "FK_Warranties_Users_OrderId",
                table: "Warranties",
                column: "OrderId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
