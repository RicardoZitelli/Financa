using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Financa.Data.Migrations
{
    public partial class _03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corretoras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corretoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaTipos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaTipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proventos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataFechamento = table.Column<DateTime>(nullable: false),
                    ValorCarteira = table.Column<decimal>(nullable: false),
                    TotalRecebido = table.Column<decimal>(nullable: false),
                    PorcentagemReferenteAoMes = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    EmpresaTipoId = table.Column<int>(nullable: true),
                    CorretoraId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_Corretoras_CorretoraId",
                        column: x => x.CorretoraId,
                        principalTable: "Corretoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empresas_EmpresaTipos_EmpresaTipoId",
                        column: x => x.EmpresaTipoId,
                        principalTable: "EmpresaTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Investimentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    PrecoCompra = table.Column<decimal>(nullable: false),
                    PrecoVenda = table.Column<decimal>(nullable: true),
                    Corretagem = table.Column<decimal>(nullable: true),
                    DataVenda = table.Column<DateTime>(nullable: true),
                    CorretoraId = table.Column<int>(nullable: true),
                    EmpresaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investimentos_Corretoras_CorretoraId",
                        column: x => x.CorretoraId,
                        principalTable: "Corretoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Investimentos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_CorretoraId",
                table: "Empresas",
                column: "CorretoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_EmpresaTipoId",
                table: "Empresas",
                column: "EmpresaTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Investimentos_CorretoraId",
                table: "Investimentos",
                column: "CorretoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Investimentos_EmpresaId",
                table: "Investimentos",
                column: "EmpresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investimentos");

            migrationBuilder.DropTable(
                name: "Proventos");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Corretoras");

            migrationBuilder.DropTable(
                name: "EmpresaTipos");
        }
    }
}
