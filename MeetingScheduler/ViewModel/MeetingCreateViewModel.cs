using MeetingScheduler.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MeetingScheduler.ViewModel
{
    public class MeetingCreateViewModel
    {
        public Meeting Meeting { get; set; }
        public List<SelectListItem> AvailableRooms { get; set; }

    }
}
