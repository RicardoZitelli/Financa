using Microsoft.EntityFrameworkCore.Migrations;

namespace Financa.Data.Migrations
{
    public partial class UserIdEmVwInvestimentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string view = "ALTER VIEW vw_Investimentos                                                                                                                  " +
                "AS                                                                                                                                                     " +
                "SELECT		i.Id,i.Data,e.Ticker,i.Tipo,i.Quantidade,i.PrecoCompra                                                                                      " +
                "			,(i.Quantidade*i.PrecoCompra) [Valor_Total]                                                                                                 " +
                "			,((i.Quantidade*i.PrecoCompra)/(SELECT SUM(i.Quantidade*i.PrecoCompra) FROM Investimentos i WHERE DataVenda IS NULL) * 100) [Porcentagem]   " +
                "			,i.Corretagem                                                                                                                               " +
                "			,(SELECT SUM(i.Quantidade*i.PrecoCompra) FROM Investimentos i WHERE DataVenda IS NULL) [Valor_Total_Investimento]                           " +
                "			,u.Id [UserId]                                                                                                                              " +
                "FROM		Investimentos i                                                                                                                             " +
                "			INNER JOIN Corretoras c ON i.CorretoraId = c.Id                                                                                             " +
                "			INNER JOIN Empresas e ON i.EmpresaId = e.Id			                                                                                        " +
                "			INNER JOIN AspNetUsers u ON i.UserId = u.Id			                                                                                        " +
                "WHERE		DataVenda IS NULL                                                                                                                           ";
            migrationBuilder.Sql(view);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string view = @"DROP VIEW vw_Investimentos";
            migrationBuilder.Sql(view);
        }
    }
}
