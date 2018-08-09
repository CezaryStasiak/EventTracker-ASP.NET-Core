using System;

namespace EventTracker.Models
{
    public interface IEventModel
    {
        DateTime Date { get; set; }
        string Description { get; set; }
        TimeSpan Length { get; set; }
        TimeSpan StartTime { get; set; }
        string Title { get; set; }
        int UserId { get; set; }
    }
}