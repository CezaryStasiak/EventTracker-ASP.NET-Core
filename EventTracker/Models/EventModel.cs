using System;

namespace EventTracker.Models
{
    public class EventModel
    {
        public int UserId { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan Length { get; set; }
    }
    
}
