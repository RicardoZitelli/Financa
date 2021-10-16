using Microsoft.EntityFrameworkCore.Migrations;

namespace Financa.Data.Migrations
{
    public partial class altervw_carteira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @" 
                                    IF EXISTS(SELECT 1 FROM SYS.VIEWS WHERE NAME='vw_carteira' AND TYPE='v')
                                    DROP VIEW vw_carteira 
                                    GO
                                    CREATE VIEW dbo.vw_carteira  
                                    AS  
                                    SELECT	c.Descricao  
                                    		, CAST(MONTH(i.data) AS VARCHAR(2)) + '/' + CAST(YEAR(i.data) AS VARCHAR(4)) [Mês/Ano]  
                                    		, CONVERT(VARCHAR(10),i.data,103) [Data da Compra]  
                                    		,e.Ticker [Empresa]  
                                    		,i.Tipo  
                                    		,i.Quantidade  
                                    		,i.PrecoCompra  
                                    		,i.Quantidade * i.PrecoCompra [Preço de Compra]    
                                    		,u.Id [UserId]
                                    		,u.UserName
                                    FROM	Investimentos i  
                                    		INNER JOIN Empresas e ON i.EmpresaId = e.Id  
                                    		INNER JOIN Corretoras c ON i.CorretoraId = c.Id
                                    		INNER JOIN AspNetUsers u ON i.UserId = u.Id



                                    GO
                                    
                                    IF EXISTS(SELECT 1 FROM SYS.VIEWS WHERE NAME='vw_TotalInvestidoPorEmpresa' AND TYPE='v')
                                    DROP VIEW vw_TotalInvestidoPorEmpresa 
                                    GO
                                    CREATE VIEW dbo.vw_TotalInvestidoPorEmpresa  
                                    AS  
                                    SELECT Empresa,SUM([Preço de Compra])[Total Investido],SUM([Quantidade]) [Quantidade], UserId,  UserName
                                    FROM vw_carteira  
                                    GROUP BY Empresa,UserId,UserName


                                    GO

                                    ALTER PROCEDURE sp_TotalInvestidoEmpresa @UserName NVARCHAR(250)
                                     AS
                                     BEGIN
                                         DECLARE @total DECIMAL(18,2)
                                         SELECT @total = SUM([Total Investido]) FROM vw_TotalInvestidoPorEmpresa 

                                         SELECT	* 
                                                 , LEFT(ROUND([Total Investido]*100 / (SELECT SUM([Total Investido]) FROM vw_TotalInvestidoPorEmpresa),2),5) [Porcentagem]
                                                 ,@total [Total Final]
                                         FROM	vw_TotalInvestidoPorEmpresa
	                                     WHERE  UserName = @UserName
                                         ORDER BY [Total Investido] DESC
                                    END
                                ";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP VIEW vw_carteira

                                GO

                                DROP VIEW vw_TotalInvestidoPorEmpresa

                                GO

                                DROP PROCEDURE sp_TotalInvestidoEmpresa
                                ";
            migrationBuilder.Sql(procedure);

        }
    }
}
