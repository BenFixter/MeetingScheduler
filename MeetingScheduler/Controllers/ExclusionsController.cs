using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingScheduler.Data;
using MeetingScheduler.Models;

namespace MeetingScheduler.Controllers
{
    public class ExclusionsController : Controller
    {
        private readonly MeetingSchedulerContext _context;

        public ExclusionsController(MeetingSchedulerContext context)
        {
            _context = context;
        }

        // GET: Exclusions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Exclusion.ToListAsync());
        }

        // GET: Exclusions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exclusion = await _context.Exclusion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exclusion == null)
            {
                return NotFound();
            }

            return View(exclusion);
        }

        // GET: Exclusions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exclusions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate")] Exclusion exclusion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exclusion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exclusion);
        }

        // GET: Exclusions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exclusion = await _context.Exclusion.FindAsync(id);
            if (exclusion == null)
            {
                return NotFound();
            }
            return View(exclusion);
        }

        // POST: Exclusions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate")] Exclusion exclusion)
        {
            if (id != exclusion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exclusion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExclusionExists(exclusion.Id))
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
            return View(exclusion);
        }

        // GET: Exclusions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exclusion = await _context.Exclusion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exclusion == null)
            {
                return NotFound();
            }

            return View(exclusion);
        }

        // POST: Exclusions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exclusion = await _context.Exclusion.FindAsync(id);
            _context.Exclusion.Remove(exclusion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExclusionExists(int id)
        {
            return _context.Exclusion.Any(e => e.Id == id);
        }
    }
}
