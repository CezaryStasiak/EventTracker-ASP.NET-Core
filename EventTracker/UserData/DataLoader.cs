using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventTracker.DateData;
using EventTracker.Models;

namespace EventTracker.UserData
{
    public class DataLoader
    {
        public List<EventModel> events { get; private set; }

        public DataLoader()
        {
            events = new List<EventModel>();
            Load();
        }

        private void Load()
        {
            // dummy data loading...
            var day1 = new Date() { Year = 2017, Month = 5, Day = 25 };
            var day2 = new Date() { Year = 2017, Month = 5, Day = 27 };
            var day3 = new Date() { Year = 2017, Month = 5, Day = 13 };
            var day4 = new Date() { Year = 2017, Month = 5, Day = 25 };
            var day5 = new Date() { Year = 2017, Month = 5, Day = 25 };
            events.Add(new EventModel() { Title = "Something1", Description = "Doing something1", Date = day1 });
            events.Add(new EventModel() { Title = "Something2", Description = "Doing something2", Date = day1 });
            events.Add(new EventModel() { Title = "Something3", Description = "Doing something3", Date = day2 });
            events.Add(new EventModel() { Title = "Something4", Description = "Doing something4", Date = day2 });
            events.Add(new EventModel() { Title = "Something5", Description = "Doing something5", Date = day3 });
            events.Add(new EventModel() { Title = "Something6", Description = "Doing something6", Date = day3 });
        }
    }
}
