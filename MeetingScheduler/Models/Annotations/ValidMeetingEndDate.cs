using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingScheduler.Models.Annotations
{
    public class ValidMeetingEndDate : ValidationAttribute
    {

        protected override ValidationResult IsValid(object objValue,
                                                       ValidationContext validationContext)
        {
            var dateValue = objValue as DateTime? ?? new DateTime();

            //alter this as needed. I am doing the date comparison if the value is not null

            if (dateValue.Date < DateTime.Now.Date )
            {
                return new ValidationResult("Date should be not be in the past");
            }

            return ValidationResult.Success;
        }
    }
}
