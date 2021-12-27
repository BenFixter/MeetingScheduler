using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingScheduler.Data;
using MeetingScheduler.Models;
using MeetingScheduler.ViewModel;

namespace MeetingScheduler.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly MeetingSchedulerContext _context;

        public MeetingsController(MeetingSchedulerContext context)
        {
            _context = context;
        }
        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            var meetings = _context.Meeting;

            await meetings.ForEachAsync(meeting =>
             {
                //Get MeetingUsers for the meeting
                List<MeetingUser> meetingUsers = _context.MeetingUser.Where(attendee => attendee.MeetingId == meeting.Id).ToList();

                meetingUsers.ForEach(meetingUser =>
                {
                    //Get the user from the meeting user
                    meetingUser.User = _context.User.Where(user => user.Id == meetingUser.UserId).First();
                });

                 meeting.MeetingUsers = meetingUsers;
             });

            return View(meetings.ToList());
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            //Get MeetingUsers for the meeting
            List<MeetingUser> meetingUsers = _context.MeetingUser.Where(attendee => attendee.MeetingId == meeting.Id).ToList();


            meetingUsers.ForEach(meetingUser =>
            {
                //Get the user from the meeting user
                meetingUser.User = _context.User.Where(user => user.Id == meetingUser.UserId).First();
            });

            meeting.MeetingUsers = meetingUsers;

            //Get room
            // Meeting room ralationship broke :(
            //ViewBag.RoomName = _context.Room.Where(room => room.Id == meeting.Room.Id).First().Name;
            return View(meeting);
        }

        // GET: Meetings/Create
        public IActionResult Create()
        {
            
            MeetingCreateViewModel vm = new MeetingCreateViewModel()
            {
                //Create a new meeting
                Meeting = new Meeting(),
                //Get list of rooms from the table rooms where the roomid doesnt exist in the meeting table
                AvailableRooms = new List<SelectListItem> {
                    new SelectListItem { Value = "1", Text = "Room1" },
                    new SelectListItem { Value = "2", Text = "Room2" },
                    new SelectListItem { Value = "3", Text = "Room3"  },
                }
            };
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,startDate,endDate")] Meeting meeting)
        {
            //Conflicts to check:
            //
            //      If the meeting has been created at the same time/day
            //          Get all records from the meeting table
            //          If the model.Meeting.startDate is found in the table, check if its in the same room
            //          If the model.Meeting.endDate is found in the table, check if its in the same room
            //
            //      Meeting Process
            //          get all meetings where model.Meeting.Room.Id is found
            //
            //      Check if meetings time slot is available:
            //          if startDate >= meetingEnd || startDate <= meetingEnd
            //              Check preferences
            //              Check exclusions
            //
            //      Check Preferences:
            //          loop through all users preference sets
            //
            //          preferenceResult = totalUsers / 100 * count
            //          while preferenceResult > 60
            //              if startDate >= preferenceEnd || startDate <= preferenceEnd is true
            //                  count++
            //                  break;
            //
            //      Check Exclusions:   
            //          loop through all users exclusion sets
            //          
            //          exclusionResult = totalUsers / 100 * count
            //          while exclusionResult > 40
            //              if startDate >= excludedEnd || startDate <= excludedEnd is false
            //                  count++
            //                  break
            //
            //      if preferenceResult > 60 && exclusionResult < 40
            //          schedule the meeting
            //      else
            //         add days and keep the same time untill the date is found

            if (ModelState.IsValid)
            {
                    _context.Add(meeting);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,startDate,endTime")] Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.Id))
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
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meeting = await _context.Meeting.FindAsync(id);
            _context.Meeting.Remove(meeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
            return _context.Meeting.Any(e => e.Id == id);
        }
    }
}
