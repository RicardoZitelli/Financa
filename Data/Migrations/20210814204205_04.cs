using Microsoft.EntityFrameworkCore.Migrations;

namespace Financa.Data.Migrations
{
    public partial class _04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Corretoras_CorretoraId",
                table: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_CorretoraId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "CorretoraId",
                table: "Empresas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorretoraId",
                table: "Empresas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_CorretoraId",
                table: "Empresas",
                column: "CorretoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Corretoras_CorretoraId",
                table: "Empresas",
                column: "CorretoraId",
                principalTable: "Corretoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
