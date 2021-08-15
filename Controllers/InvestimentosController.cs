using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Financa.Data;
using Financa.Models;

namespace Financa.Controllers
{
    public class InvestimentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvestimentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Investimentos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Investimentos.Include(i => i.Corretora).Include(i => i.Empresa);
            return View(await applicationDbContext.ToListAsync());
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
    }
}
