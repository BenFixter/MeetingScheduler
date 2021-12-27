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
    public class PreferencesController : Controller
    {
        private readonly MeetingSchedulerContext _context;

        public PreferencesController(MeetingSchedulerContext context)
        {
            _context = context;
        }

        // GET: Preferences
        public async Task<IActionResult> Index()
        {
            return View(await _context.Preference.ToListAsync());
        }

        // GET: Preferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preference = await _context.Preference
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preference == null)
            {
                return NotFound();
            }

            return View(preference);
        }

        // GET: Preferences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Preferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate")] Preference preference)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preference);
        }

        // GET: Preferences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preference = await _context.Preference.FindAsync(id);
            if (preference == null)
            {
                return NotFound();
            }
            return View(preference);
        }

        // POST: Preferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate")] Preference preference)
        {
            if (id != preference.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreferenceExists(preference.Id))
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
            return View(preference);
        }

        // GET: Preferences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preference = await _context.Preference
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preference == null)
            {
                return NotFound();
            }

            return View(preference);
        }

        // POST: Preferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preference = await _context.Preference.FindAsync(id);
            _context.Preference.Remove(preference);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreferenceExists(int id)
        {
            return _context.Preference.Any(e => e.Id == id);
        }
    }
}
