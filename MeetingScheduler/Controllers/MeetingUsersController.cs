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
    public class MeetingUsersController : Controller
    {
        private readonly MeetingSchedulerContext _context;

        public MeetingUsersController(MeetingSchedulerContext context)
        {
            _context = context;
        }

        // GET: MeetingUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.MeetingUser.ToListAsync());
        }

        // GET: MeetingUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingUser = await _context.MeetingUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetingUser == null)
            {
                return NotFound();
            }

            return View(meetingUser);
        }

        // GET: MeetingUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeetingUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,equipmentNotes,importantNotes")] MeetingUser meetingUser)
        {
            //TODO: Add User to a Meeting
            if (ModelState.IsValid)
            {
                _context.Add(meetingUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meetingUser);
        }

        // GET: MeetingUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingUser = await _context.MeetingUser.FindAsync(id);
            if (meetingUser == null)
            {
                return NotFound();
            }
            return View(meetingUser);
        }

        // POST: MeetingUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,equipmentNotes,importantNotes")] MeetingUser meetingUser)
        {
            if (id != meetingUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingUserExists(meetingUser.Id))
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
            return View(meetingUser);
        }

        // GET: MeetingUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingUser = await _context.MeetingUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetingUser == null)
            {
                return NotFound();
            }

            return View(meetingUser);
        }

        // POST: MeetingUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meetingUser = await _context.MeetingUser.FindAsync(id);
            _context.MeetingUser.Remove(meetingUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingUserExists(int id)
        {
            return _context.MeetingUser.Any(e => e.Id == id);
        }
    }
}
