using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTracker.DateData
{
    public class Time
    {
        public int Minute { get; set; }
        public int Hour { get; set; }

        public override string ToString()
        {
            return this == null ? "Time not set" : $"{Hour}:{Minute}";
        }
    }

}
