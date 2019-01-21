using System;
using TimeClassLib;
using Xunit;

namespace TimeClassLibTests {
    public class TimePeriodTests {
        [Fact]
        public void Constructor_Seconds_ShoudlReturnTimePeriod() {
            // Arrange
            byte expectedHours = 0;
            byte expectedMinutes = 0;
            byte expectedSeconds = 1;
            // Act
            var timePeriod = new TimePeriod(expectedSeconds);
            // Assert
            Assert.Equal(expectedHours, timePeriod.Hours);
            Assert.Equal(expectedMinutes, timePeriod.Minutes);
            Assert.Equal(expectedSeconds, timePeriod.Seconds);
        }

        [Fact]
        public void Constructor_MinutesSeconds_ShoudlReturnTimePeriod() {
            // Arrange
            byte expectedHours = 0;
            byte expectedMinutes = 1;
            byte expectedSeconds = 1;
            // Act
            var timePeriod = new TimePeriod(expectedMinutes, expectedSeconds);
            // Assert
            Assert.Equal(expectedHours, timePeriod.Hours);
            Assert.Equal(expectedMinutes, timePeriod.Minutes);
            Assert.Equal(expectedSeconds, timePeriod.Seconds);
        }

        [Fact]
        public void Constructor_MinutesAnd60Seconds_ShoudlThrowArgumentExcpetion() {
            // Arrange
            byte minutes = 1;
            byte tooManySeconds = 60;
            // Act
            Func<object> conctructor = () => new TimePeriod(minutes, tooManySeconds);
            // Assert
            Assert.Throws<ArgumentException>(conctructor);
        }

        [Fact]
        public void Constructor_HoursMinutesSeconds_ShoudlReturnTimePeriod() {
            // Arrange
            byte expectedHours = 1;
            byte expectedMinutes = 1;
            byte expectedSeconds = 1;
            // Act
            var time = new TimePeriod(expectedHours, expectedMinutes, expectedSeconds);
            // Assert
            Assert.Equal(expectedHours, time.Hours);
            Assert.Equal(expectedMinutes, time.Minutes);
            Assert.Equal(expectedSeconds, time.Seconds);
        }

        [Fact]
        public void Constructor_HoursMinutes60Seconds_ShoudlThrowArgumentExcpetion() {
            // Arrange
            byte hours = 0;
            byte minutes = 0;
            byte tooManySeconds = 60;
            // Act
            Action conctructor = () => new TimePeriod(hours, minutes, tooManySeconds);
            // Assert
            Assert.Throws<ArgumentException>(conctructor);
        }
    }
}
