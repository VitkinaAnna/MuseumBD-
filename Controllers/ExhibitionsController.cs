using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuseunBD;

namespace MuseunBD.Controllers
{
    public class ExhibitionsController : Controller
    {
        private readonly MuseumContext _context;

        public ExhibitionsController(MuseumContext context)
        {
            _context = context;
        }

        // GET: Exhibitions
        public async Task<IActionResult> Index()
        {
            var museumContext = _context.Exhibitions.Include(e => e.Worker);
            return View(await museumContext.ToListAsync());
        }

        // GET: Exhibitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exhibitions == null)
            {
                return NotFound();
            }

            var exhibition = await _context.Exhibitions
                .Include(e => e.Worker)
                .FirstOrDefaultAsync(m => m.ExhibitionId == id);
            if (exhibition == null)
            {
                return NotFound();
            }

            return View(exhibition);
        }

        // GET: Exhibitions/Create
        public IActionResult Create()
        {
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "Name");
            return View();
        }

        // POST: Exhibitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExhibitionId,Date,Name,WorkerId,Price")] Exhibition exhibition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exhibition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "Name", exhibition.WorkerId);
            return View(exhibition);
        }

        // GET: Exhibitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exhibitions == null)
            {
                return NotFound();
            }

            var exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition == null)
            {
                return NotFound();
            }
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "Name", exhibition.WorkerId);
            return View(exhibition);
        }

        // POST: Exhibitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExhibitionId,Date,Name,WorkerId,Price")] Exhibition exhibition)
        {
            if (id != exhibition.ExhibitionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exhibition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExhibitionExists(exhibition.ExhibitionId))
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
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "Name", exhibition.WorkerId);
            return View(exhibition);
        }

        // GET: Exhibitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exhibitions == null)
            {
                return NotFound();
            }

            var exhibition = await _context.Exhibitions
                .Include(e => e.Worker)
                .FirstOrDefaultAsync(m => m.ExhibitionId == id);
            if (exhibition == null)
            {
                return NotFound();
            }

            return View(exhibition);
        }

        // POST: Exhibitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exhibitions == null)
            {
                return Problem("Entity set 'MuseumContext.Exhibitions'  is null.");
            }
            var exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition != null)
            {
                _context.Exhibitions.Remove(exhibition);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExhibitionExists(int id)
        {
          return (_context.Exhibitions?.Any(e => e.ExhibitionId == id)).GetValueOrDefault();
        }
    }
}
