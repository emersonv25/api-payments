using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DisapprovedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Antecipado = table.Column<bool>(type: "bit", nullable: true),
                    Acquirer = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    InstallmentsNumber = table.Column<int>(type: "int", nullable: false),
                    LastFourDigitsCard = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    InstallmentNumber = table.Column<int>(type: "int", nullable: false),
                    ForecastPaymentAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    AnticipatedAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    AnticipatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installments_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installments_TransactionId",
                table: "Installments",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
