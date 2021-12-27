using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingScheduler.Models
{
    public enum Role
    {
        [Display(Name = "Meeting Initiator")]
        MEETING_INITIATOR,

        [Display(Name = "Attendee")]
        ATTENDEE
    }
}
