using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeetingScheduler.Models;

namespace MeetingScheduler.Data
{
    public class MeetingSchedulerContext : DbContext
    {
        public MeetingSchedulerContext (DbContextOptions<MeetingSchedulerContext> options)
            : base(options)
        {
        }

        public DbSet<MeetingScheduler.Models.Meeting> Meeting { get; set; }

        public DbSet<MeetingScheduler.Models.User> User { get; set; }

        public DbSet<MeetingScheduler.Models.Comment> Comment { get; set; }

        public DbSet<MeetingScheduler.Models.MeetingUser> MeetingUser { get; set; }

        public DbSet<MeetingScheduler.Models.Room> Room { get; set; }

        public DbSet<MeetingScheduler.Models.Exclusion> Exclusion { get; set; }

        public DbSet<MeetingScheduler.Models.Preference> Preference { get; set; }
    }
}
