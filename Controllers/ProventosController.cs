using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Financa.Data;
using Financa.Models;
using Microsoft.AspNetCore.Authorization;

namespace Financa.Controllers
{
    [Authorize]
    public class ProventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proventos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proventos.ToListAsync());
        }

        // GET: Proventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provento = await _context.Proventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provento == null)
            {
                return NotFound();
            }

            return View(provento);
        }

        // GET: Proventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataFechamento,ValorCarteira,TotalRecebido,PorcentagemReferenteAoMes")] Provento provento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provento);
        }

        // GET: Proventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provento = await _context.Proventos.FindAsync(id);
            if (provento == null)
            {
                return NotFound();
            }
            return View(provento);
        }

        // POST: Proventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataFechamento,ValorCarteira,TotalRecebido,PorcentagemReferenteAoMes")] Provento provento)
        {
            if (id != provento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProventoExists(provento.Id))
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
            return View(provento);
        }

        // GET: Proventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provento = await _context.Proventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provento == null)
            {
                return NotFound();
            }

            return View(provento);
        }

        // POST: Proventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provento = await _context.Proventos.FindAsync(id);
            _context.Proventos.Remove(provento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProventoExists(int id)
        {
            return _context.Proventos.Any(e => e.Id == id);
        }
    }
}
