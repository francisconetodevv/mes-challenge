using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MES.Rastreabilidade.Infrastructure.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessStepsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EtapasDoProcessos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapasDoProcessos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistroDeEtapas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateInitial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    EtapaDoProcessoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroDeEtapas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroDeEtapas_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroDeEtapas_EtapasDoProcessos_EtapaDoProcessoId",
                        column: x => x.EtapaDoProcessoId,
                        principalTable: "EtapasDoProcessos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

                migrationBuilder.InsertData(
                table: "EtapasDoProcessos",
                columns: new[] { "Id", "Name", "Order" },
                values: new object[,]
                {
                    { 1, "Pesagem e Dosagem", 1 },
                    { 2, "Mistura e Homogeneização", 2 },
                    { 3, "Reação Catalítica", 3 },
                    { 4, "Filtração e Purificação", 4 },
                    { 5, "Controle de Qualidade", 5 },
                    { 6, "Envase e Rotulagem", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroDeEtapas_BatchId",
                table: "RegistroDeEtapas",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroDeEtapas_EtapaDoProcessoId",
                table: "RegistroDeEtapas",
                column: "EtapaDoProcessoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroDeEtapas");

            migrationBuilder.DropTable(
                name: "EtapasDoProcessos");
        }
    }
}
