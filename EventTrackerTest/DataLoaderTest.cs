using System;
using EventTracker.UserData;
using Xunit;

namespace EventTrackerTest
{
    public class DataLoaderTest
    {
        
        [Fact]
        public void LoadFileTest_NoFileExceptionExpected()
        {
            DataLoader data = new DataLoader();
            Assert.Throws<System.IO.FileNotFoundException>(() => data.Load("file"));
        }

        [Fact]
        public void LoadFileTest_SuccessExpected()
        {
            DataLoader data = new DataLoader();
            var events = data.Load("Data.txt");
            Assert.Equal(4, events.Count);
        }

        [Fact]
        public void LoadFileTest_RightValuesExpected()
        {
            DataLoader data = new DataLoader();
            var events = data.Load("Data.txt");
            Assert.Equal("Birthday", events[0].Title);
            Assert.Equal("Watch football Match", events[2].Description);
            Assert.Equal(new DateTime(2018,05,31,16,00,00), events[1].StartTime);
        }
    }
}
