using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EventTracker.DateData;

namespace EventTracker.Models
{
    public class EventModel
    {
        public int UserId { get; set; }

        [Required]
        [Range(5,25)]
        public string Title { get; set; }

        public string Description { get; set; }

        public Date Date { get; set; }

        public Time StartTime { get; set; }

        public Time Length { get; set; }
    }
    
}
