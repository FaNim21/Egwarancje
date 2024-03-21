using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgwarancjeDbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class ModelsUpdates1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdersSpec",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Realization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueNet = table.Column<float>(type: "real", nullable: false),
                    ValueGross = table.Column<float>(type: "real", nullable: false),
                    WarrantyLength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersSpec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersSpec_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersSpec_OrderId",
                table: "OrdersSpec",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrdersSpec");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");
        }
    }
}
