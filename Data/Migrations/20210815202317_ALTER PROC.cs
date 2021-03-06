using Microsoft.EntityFrameworkCore.Migrations;

namespace Financa.Data.Migrations
{
    public partial class ALTERPROC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"   ALTER PROCEDURE sp_TotalInvestidoEmpresa
                                    AS
                                    BEGIN
	                                    DECLARE @total DECIMAL(18,2)

	                                    SELECT @total = SUM([Total Investido]) FROM vw_TotalInvestidoPorEmpresa 

	                                    SELECT	* 
			                                    , LEFT(ROUND([Total Investido]*100 / (SELECT SUM([Total Investido]) FROM vw_TotalInvestidoPorEmpresa),2),5) [Porcentagem]
			                                    ,@total [Total Final]
	                                    FROM	vw_TotalInvestidoPorEmpresa
	                                    ORDER BY Empresa
                                    END";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE sp_TotalInvestidoEmpresa";
            migrationBuilder.Sql(procedure);

        }
    }
}
