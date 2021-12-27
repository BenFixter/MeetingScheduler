using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingScheduler.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime openedDate { get; set; }
        public DateTime closedDate { get; set; }
        public List<Meeting> Meetings { get; set; }
    }
}