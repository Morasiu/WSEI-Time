using System;
using System.Linq;
using TimeClassLib;
using Xunit;

namespace TimeClassLibTests {
    public class TimeTests {
        [Fact]
        public void Constructor_Hours_ShoudlReturnTime() {
            // Arrange
            byte expectedHours = 1;
            byte expectedMinutes = 0;
            byte expectedSeconds = 0;
            // Act
            var time = new Time(expectedHours);
            // Assert
            Assert.Equal(expectedHours, time.Hours);
            Assert.Equal(expectedMinutes, time.Minutes);
            Assert.Equal(expectedSeconds, time.Seconds);
        }

        [Fact]
        public void Constructor_26Hours_ShoudlThrowArgumentExcpetion() {
            // Arrange
            byte tooManyHours = 26;
            // Act
            Action conctructor = () => new Time(tooManyHours);
            // Assert
            Assert.Throws<ArgumentException>(conctructor);
        }

        [Fact]
        public void Constructor_HoursMinutes_ShoudlReturnTime() {
            // Arrange
            byte expectedHours = 1;
            byte expectedMinutes = 1;
            byte expectedSeconds = 0;
            // Act
            var time = new Time(expectedHours, expectedMinutes);
            // Assert
            Assert.Equal(expectedHours, time.Hours);
            Assert.Equal(expectedMinutes, time.Minutes);
            Assert.Equal(expectedSeconds, time.Seconds);
        }

        [Fact]
        public void Constructor_Hours60Minutes_ShoudlThrowArgumentExcpetion() {
            // Arrange
            byte hours = 1;
            byte tooManyMminutes = 60;
            // Act
            Action conctructor = () => new Time(hours, tooManyMminutes);
            // Assert
            Assert.Throws<ArgumentException>(conctructor);
        }

        [Fact]
        public void Constructor_HoursMinutesSeconds_ShoudlReturnTime() {
            // Arrange
            byte expectedHours = 1;
            byte expectedMinutes = 1;
            byte expectedSeconds = 1;
            // Act
            var time = new Time(expectedHours, expectedMinutes, expectedSeconds);
            // Assert
            Assert.Equal(expectedHours, time.Hours);
            Assert.Equal(expectedMinutes, time.Minutes);
            Assert.Equal(expectedSeconds, time.Seconds);
        }

        [Fact]
        public void Constructor_HoursMinutes60Seconds_ShoudlThrowArgumentExcpetion() {
            // Arrange
            byte hours = 1;
            byte minutes = 60;
            byte tooManySeconds = 60;
            // Act
            Action conctructor = () => new Time(hours, minutes, tooManySeconds);
            // Assert
            Assert.Throws<ArgumentException>(conctructor);
        }

        [Fact]
        public void Contructor_CorrectString_ShouldReturnTime() {
            // Arrange
            string formattedTime = "10:10:10";
            // Act
            var time = new Time(formattedTime);
            // Assert
            Assert.Equal(10, time.Hours);
            Assert.Equal(10, time.Minutes);
            Assert.Equal(10, time.Seconds);
        }

        [Fact]
        public void Contructor_WrongStringFormat_ShouldThrowArgumentExcpetion() {
            // Arrange
            string formattedTime = "10-10-10";
            // Act
            Action contructor = () => new Time(formattedTime);
            // Assert
            Assert.Throws<ArgumentException>(contructor);
        }

        [Fact]
        public void Contructor_EmptyString_ShouldThrowArgumentExcpetion() {
            // Arrange
            string emptyString = string.Empty;
            // Act
            Action contructor = () => new Time(emptyString);
            // Assert
            Assert.Throws<ArgumentException>(contructor);
        }

        [Fact]
        public void Contructor_StringWithTooLargeValues_ShouldThrowArgumentExcpetion() {
            // Arrange
            string stringWithTooLargeValues = "24:60:60";
            // Act
            Action contructor = () => new Time(stringWithTooLargeValues);
            // Assert
            Assert.Throws<ArgumentException>(contructor);
        }

        [Fact]
        public void Properties_Readonly_ShouldBeImmutable() {
            // Arrange
            var properties = typeof(Time).GetProperties();
            // Act
            // Assert
            Assert.True(properties.Length == 3);
            foreach(var property in properties) {
                Assert.False(property.CanWrite);
            }
        }


    }
}
