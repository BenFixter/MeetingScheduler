using MeetingScheduler.Models.Annotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingScheduler.Models
{
    public class Meeting : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public IEnumerable<MeetingUser> MeetingUsers { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        //one to many relationship gives an error :(
        //public int RoomId { get; set; }
        //public Room Room { get; set; }

        //https://stackoverflow.com/questions/21777412/mvc-model-validation-for-date
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //List of errors that get sent to the view
            List<ValidationResult> results = new List<ValidationResult>();

            if (startDate < DateTime.Now)
            {
                results.Add(new ValidationResult("Start date/time must be greater than the current date/time", new[] { "startDate" }));
            }

            if (endDate <= startDate)
            {
                results.Add(new ValidationResult("End date/time must be greater that start date/time", new[] { "endDate" }));
            }

            return results;
        }
    }
}
