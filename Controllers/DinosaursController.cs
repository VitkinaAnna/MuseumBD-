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
    public class DinosaursController : Controller
    {
        private readonly MuseumContext _context;

        public DinosaursController(MuseumContext context)
        {
            _context = context;
        }

        // GET: Dinosaurs
        public async Task<IActionResult> Index()
        {
            var museumContext = _context.Dinosaurs.Include(d => d.Hall);
            return View(await museumContext.ToListAsync());
        }

        // GET: Dinosaurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dinosaurs == null)
            {
                return NotFound();
            }

            var dinosaur = await _context.Dinosaurs
                .Include(d => d.Hall)
                .FirstOrDefaultAsync(m => m.DinosaurId == id);
            if (dinosaur == null)
            {
                return NotFound();
            }

            return View(dinosaur);
        }

        // GET: Dinosaurs/Create
        public IActionResult Create()
        {
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "Name");
            return View();
        }

        // POST: Dinosaurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DinosaurId,Name,HallId,Lifetime")] Dinosaur dinosaur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dinosaur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "Name", dinosaur.HallId);
            return View(dinosaur);
        }

        // GET: Dinosaurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dinosaurs == null)
            {
                return NotFound();
            }

            var dinosaur = await _context.Dinosaurs.FindAsync(id);
            if (dinosaur == null)
            {
                return NotFound();
            }
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "Name", dinosaur.HallId);
            return View(dinosaur);
        }

        // POST: Dinosaurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DinosaurId,Name,HallId,Lifetime")] Dinosaur dinosaur)
        {
            if (id != dinosaur.DinosaurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dinosaur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DinosaurExists(dinosaur.DinosaurId))
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
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "Name", dinosaur.HallId);
            return View(dinosaur);
        }

        // GET: Dinosaurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dinosaurs == null)
            {
                return NotFound();
            }

            var dinosaur = await _context.Dinosaurs
                .Include(d => d.Hall)
                .FirstOrDefaultAsync(m => m.DinosaurId == id);
            if (dinosaur == null)
            {
                return NotFound();
            }

            return View(dinosaur);
        }

        // POST: Dinosaurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dinosaurs == null)
            {
                return Problem("Entity set 'MuseumContext.Dinosaurs'  is null.");
            }
            var dinosaur = await _context.Dinosaurs.FindAsync(id);
            if (dinosaur != null)
            {
                _context.Dinosaurs.Remove(dinosaur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DinosaurExists(int id)
        {
          return (_context.Dinosaurs?.Any(e => e.DinosaurId == id)).GetValueOrDefault();
        }
    }
}
