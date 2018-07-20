using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventTracker.Models
{
    public class EventModel
    {
        public int UserId { get; set; }

        [Required]
        [Range(5,25)]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan Length { get; set; }
    }
    
}
