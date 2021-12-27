using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingScheduler.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public IEnumerable<MeetingUser> MeetingUsers { get; set; }
        public bool isImportant { get; set; }
        [NotMapped]
        public List<Exclusion> exclusionSet { get; set; }
        [NotMapped]
        public List<Preference> preferenceSet { get; set; }

        public User()
        {
            exclusionSet = new List<Exclusion>();
            preferenceSet = new List<Preference>();
        }
    }
}