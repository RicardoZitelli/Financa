using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Financa.Data;
using Financa.Models;

namespace Financa.Controllers
{
    public class EmpresaTiposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpresaTiposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmpresaTipos
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpresaTipos.ToListAsync());
        }

        // GET: EmpresaTipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaTipo = await _context.EmpresaTipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresaTipo == null)
            {
                return NotFound();
            }

            return View(empresaTipo);
        }

        // GET: EmpresaTipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpresaTipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] EmpresaTipo empresaTipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresaTipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresaTipo);
        }

        // GET: EmpresaTipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaTipo = await _context.EmpresaTipos.FindAsync(id);
            if (empresaTipo == null)
            {
                return NotFound();
            }
            return View(empresaTipo);
        }

        // POST: EmpresaTipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] EmpresaTipo empresaTipo)
        {
            if (id != empresaTipo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresaTipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaTipoExists(empresaTipo.Id))
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
            return View(empresaTipo);
        }

        // GET: EmpresaTipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaTipo = await _context.EmpresaTipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresaTipo == null)
            {
                return NotFound();
            }

            return View(empresaTipo);
        }

        // POST: EmpresaTipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresaTipo = await _context.EmpresaTipos.FindAsync(id);
            _context.EmpresaTipos.Remove(empresaTipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaTipoExists(int id)
        {
            return _context.EmpresaTipos.Any(e => e.Id == id);
        }
    }
}
