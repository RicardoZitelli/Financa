using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Financa.Data;
using Financa.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

using ExcelDataReader;
using YahooFinanceApi;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Financa.Controllers
{
    public class InvestimentosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string stringConnection;

        public InvestimentosController(ApplicationDbContext context)
        {
            _context = context;
            stringConnection = "Server=DESKTOP-UT5R5SE;Database=Financa;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public async Task<Acao> getStockData(string symbol)
        {
            Acao acao = new Acao();

            try
            {
                //string ticker = Request.Query["symbol"];
                string ticker = symbol;

                string param = ticker + ".SA";

                var security = await Yahoo.Symbols(param).QueryAsync();

                List<Security> securities = security.Values.ToList();

                foreach (var item in securities)
                {
                    acao.Ask = item.Ask;
                    acao.Bid = item.Bid;
                    acao.NameCompany = item.LongName;
                    acao.ShortName = item.ShortName;
                    acao.Symbol = item.Symbol;
                    acao.Ticker = ticker;
                    acao.RegularMarketChangePercent = item.RegularMarketChangePercent;
                }

            }
            catch (Exception ex)
            {

                acao.erro = ex;
            }

            return acao;
        }

        // GET: Investimentos
        public async Task<IActionResult> Index()
        {
            var investimentos = _context.Investimentos.Include(i => i.Corretora).Include(i => i.Empresa);

            var vw_investimento = vw_Investimentos();

            foreach (Investimento item in investimentos)
            {
                Task<Acao> acaoAsync = getStockData(item.Empresa.Ticker);

                Acao acao = new Acao();

                if (acaoAsync.Result.erro == null)
                {
                    acao = acaoAsync.Result;

                    item.Acao = acao;

                    item.Valor_Total = vw_investimento.Where(i => i.Id == item.Id).Select(i => i.Valor_Total).FirstOrDefault();

                    item.Porcentagem = vw_investimento.Where(i => i.Id == item.Id).Select(i => i.Porcentagem).FirstOrDefault();

                    item.Valor_Total_Investimento = vw_investimento.Where(i => i.Id == item.Id).Select(i => i.Valor_Total_Investimento).FirstOrDefault();

                    item.ValorCarteira = acao.Bid * item.Quantidade;

                    item.Valorizacao = item.ValorCarteira - double.Parse(item.Valor_Total.ToString()) - double.Parse(item.Corretagem.ToString());
                    
                    item.ValorizacaoPercentual = ((((item.ValorCarteira-double.Parse(item.Corretagem.ToString()))) / double.Parse(item.Valor_Total.ToString()))-1)*100;

                }

            }

            return View(await investimentos.OrderBy(i => i.Empresa.Ticker).ToListAsync());
        }

        public List<vw_Investimentos> vw_Investimentos()
        {
            List<vw_Investimentos> listaInvestimentos = new List<vw_Investimentos>();

            using (SqlConnection sql = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM vw_Investimentos ORDER BY Ticker", sql))
                {
                    cmd.CommandType = CommandType.Text;

                    sql.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var investimento = new vw_Investimentos((int)reader[0]
                            , DateTime.Parse(reader[1].ToString())
                            , reader[2].ToString()
                            , reader[3].ToString()
                            , int.Parse(reader[4].ToString())
                            , decimal.Parse(reader[5].ToString())
                            , decimal.Parse(reader[6].ToString())
                            , double.Parse(reader[7].ToString())
                            , decimal.Parse(reader[8].ToString())
                            , decimal.Parse(reader[9].ToString()));

                            listaInvestimentos.Add(investimento);
                        }
                    }
                }
            }
            return listaInvestimentos;
        }

        // GET: Investimentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investimento = await _context.Investimentos
                .Include(i => i.Corretora)
                .Include(i => i.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investimento == null)
            {
                return NotFound();
            }

            return View(investimento);
        }

        // GET: Investimentos/Create
        public IActionResult Create()
        {
            ViewData["CorretoraId"] = new SelectList(_context.Corretoras, "Id", "Descricao");
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Ticker");
            return View();
        }

        // POST: Investimentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Tipo,Quantidade,PrecoCompra,PrecoVenda,Corretagem,DataVenda,CorretoraId,EmpresaId")] Investimento investimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(investimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CorretoraId"] = new SelectList(_context.Corretoras, "Id", "Descricao", investimento.CorretoraId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Ticker", investimento.EmpresaId);
            return View(investimento);
        }

        // GET: Investimentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investimento = await _context.Investimentos.FindAsync(id);
            if (investimento == null)
            {
                return NotFound();
            }
            ViewData["CorretoraId"] = new SelectList(_context.Corretoras, "Id", "Descricao", investimento.CorretoraId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Ticker", investimento.EmpresaId);
            return View(investimento);
        }

        // POST: Investimentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Tipo,Quantidade,PrecoCompra,PrecoVenda,Corretagem,DataVenda,CorretoraId,EmpresaId")] Investimento investimento)
        {
            if (id != investimento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestimentoExists(investimento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CorretoraId"] = new SelectList(_context.Corretoras, "Id", "Descricao", investimento.CorretoraId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Ticker", investimento.EmpresaId);
            return View(investimento);
        }

        // GET: Investimentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investimento = await _context.Investimentos
                .Include(i => i.Corretora)
                .Include(i => i.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investimento == null)
            {
                return NotFound();
            }

            return View(investimento);
        }

        // POST: Investimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var investimento = await _context.Investimentos.FindAsync(id);
            _context.Investimentos.Remove(investimento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestimentoExists(int id)
        {
            return _context.Investimentos.Any(e => e.Id == id);
        }

        private bool InvestimentoExists(DateTime data, decimal precoCompra, Empresa empresa, Corretora corretora)
        {
            return _context.Investimentos.Any(e => e.Data == data && e.PrecoCompra == precoCompra && e.CorretoraId == corretora.Id && e.EmpresaId == empresa.Id);
        }

        [HttpPost]
        public IActionResult InsereViaExcel(IFormFile file)
        {
            List<Investimento> investimentos = new List<Investimento>();
            if (file.Length > 0)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var stream = file.OpenReadStream())
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        bool cabecalho = true;

                        while (reader.Read())
                        {
                            if (!cabecalho)
                            {
                                var corretora = _context.Corretoras.Where(c => c.Descricao == reader.GetValue(0).ToString()).FirstOrDefault();
                                var empresa = _context.Empresas.Where(e => e.Ticker == reader.GetValue(1).ToString()).FirstOrDefault();

                                empresa = GravaEmpresa(reader, empresa);
                                corretora = GravaCorretora(reader, corretora);

                                Investimento investimento = new Investimento()
                                {
                                    Id = 0,
                                    Data = (DateTime)reader.GetValue(2),
                                    Tipo = reader.GetValue(3).ToString(),
                                    Quantidade = int.Parse(reader.GetValue(4).ToString()),
                                    PrecoCompra = decimal.Parse(reader.GetValue(5).ToString()),
                                    PrecoVenda = 0,
                                    Corretagem = 0,
                                    DataVenda = null,
                                    CorretoraId = corretora.Id,
                                    EmpresaId = empresa.Id,
                                    Corretora = corretora,
                                    Empresa = empresa
                                };

                                if (!InvestimentoExists(investimento.Data, investimento.PrecoCompra, investimento.Empresa, investimento.Corretora))
                                {
                                    investimentos.Add(investimento);
                                }
                            }

                            cabecalho = false;
                        }
                    }
                }

                investimentos.ForEach(i => _context.Investimentos.Add(i));

                _context.SaveChanges();
            }

            var applicationDbContext = _context.Investimentos.Include(i => i.Corretora).Include(i => i.Empresa);
            return View(nameof(Index), applicationDbContext.ToList());
        }

        private Empresa GravaEmpresa(IExcelDataReader reader, Empresa empresa)
        {
            if (empresa == null)
            {
                empresa = new Empresa()
                {
                    Id = 0,
                    Ticker = reader.GetValue(1).ToString(),
                    Nome = reader.GetValue(1).ToString(),

                };

                _context.Add(empresa);
                _context.SaveChanges();

                empresa = _context.Empresas.Where(e => e.Ticker == reader.GetValue(1).ToString()).FirstOrDefault();
            }

            return empresa;
        }

        private Corretora GravaCorretora(IExcelDataReader reader, Corretora corretora)
        {
            if (corretora == null)
            {
                corretora = new Corretora()
                {
                    Id = 0,
                    Descricao = reader.GetValue(1).ToString()
                };

                _context.Add(corretora);
                _context.SaveChanges();

                corretora = _context.Corretoras.Where(e => e.Descricao == reader.GetValue(1).ToString()).FirstOrDefault();
            }

            return corretora;
        }
    }
}
