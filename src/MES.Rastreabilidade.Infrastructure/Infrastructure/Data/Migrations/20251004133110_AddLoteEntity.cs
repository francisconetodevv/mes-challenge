using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MES.Rastreabilidade.Infrastructure.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLoteEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtyProduced = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateInitial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductionOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batches_ProductionOrders_ProductionOrderId",
                        column: x => x.ProductionOrderId,
                        principalTable: "ProductionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ProductionOrderId",
                table: "Batches",
                column: "ProductionOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Batches");
        }
    }
}
