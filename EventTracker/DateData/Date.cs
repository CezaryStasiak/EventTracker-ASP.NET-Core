using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTracker.DateData
{
    public class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        override public string ToString()
        {
            return $"{Year} / {Month} / {Day} ";
        } 
    }

}
