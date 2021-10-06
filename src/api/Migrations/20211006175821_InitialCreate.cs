using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    IdTransacao = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataTransacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAprovacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataReprovacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Antecipado = table.Column<bool>(type: "bit", nullable: true),
                    Adquirente = table.Column<bool>(type: "bit", nullable: true),
                    ValorBruto = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ValorLiquido = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Taxa = table.Column<float>(type: "real", nullable: false),
                    NumeroParcelas = table.Column<int>(type: "int", nullable: false),
                    UltimosQuatroDigitos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.IdTransacao);
                });

            migrationBuilder.CreateTable(
                name: "Parcelas",
                columns: table => new
                {
                    IdParcela = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorBruto = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ValorLiquido = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NumeroParcela = table.Column<int>(type: "int", nullable: false),
                    ValorAntecipado = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    PrevisaoRecebimentoData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepasseData = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTransacao = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcelas", x => x.IdParcela);
                    table.ForeignKey(
                        name: "FK_Parcelas_Transacoes_IdTransacao",
                        column: x => x.IdTransacao,
                        principalTable: "Transacoes",
                        principalColumn: "IdTransacao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcelas_IdTransacao",
                table: "Parcelas",
                column: "IdTransacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcelas");

            migrationBuilder.DropTable(
                name: "Transacoes");
        }
    }
}
