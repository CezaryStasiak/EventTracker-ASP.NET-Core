using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventTracker.Models;

namespace EventTracker.UserData
{
    public class DataLoader
    {
        /// <summary>
        /// Loads data from text file where each line represent one event in format : title-description-startTime(eg. 15:00/23:05:2018)-endTime
        /// </summary>
        /// <param name="filePath">Text file with data</param>
        /// <returns>Returns list of loaded events</returns>
        public List<EventModel> Load(string filePath)
        {
            List<EventModel> events = new List<EventModel>();
            string[] buffer = new string[5];
            try
            {  
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            string line = sr.ReadLine();
                            buffer = line.Split("-").ToArray();
                            int years = int.Parse(buffer[2].Split("/").ToArray().ElementAt(1).Split(":").ToArray().ElementAt(2));
                            int months = int.Parse(buffer[2].Split("/").ToArray().ElementAt(1).Split(":").ToArray().ElementAt(1));
                            int days = int.Parse(buffer[2].Split("/").ToArray().ElementAt(1).Split(":").ToArray().ElementAt(0));
                            int hours = int.Parse(buffer[2].Split("/").ToArray().ElementAt(0).Split(":").ToArray().ElementAt(0));
                            int minutes = int.Parse(buffer[2].Split("/").ToArray().ElementAt(0).Split(":").ToArray().ElementAt(1));
                            DateTime start = new DateTime(years, months, days, hours, minutes, 0);
                            years = int.Parse(buffer[3].Split("/").ToArray().ElementAt(1).Split(":").ToArray().ElementAt(2));
                            months = int.Parse(buffer[3].Split("/").ToArray().ElementAt(1).Split(":").ToArray().ElementAt(1));
                            days = int.Parse(buffer[3].Split("/").ToArray().ElementAt(1).Split(":").ToArray().ElementAt(0));
                            hours = int.Parse(buffer[3].Split("/").ToArray().ElementAt(0).Split(":").ToArray().ElementAt(0));
                            minutes = int.Parse(buffer[3].Split("/").ToArray().ElementAt(0).Split(":").ToArray().ElementAt(1));
                            DateTime end = new DateTime(years, months, days, hours, minutes, 0);
                            events.Add(new EventModel { Title = buffer[0], Description = buffer[1], StartTime = start, EndTime = end });
                        }
                        catch (InvalidDataException e)
                        {
                            Console.WriteLine("Data format is invalid !" + e.Message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read");
                Console.WriteLine(e.Message);
            }

            return events;
        }
    }
}
