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

        [Fact]
        public void Constructor_TwoTimes_ShoudlReturnTimePeriod() {
            // Arrange
            var firstTime = new Time(1, 1, 1);
            var secondTime = new Time(2, 2, 2);
            var expectedTimePeriod = new TimePeriod(1, 1, 1);
            // Act
            var result = new TimePeriod(firstTime, secondTime);
            // Assert
            Assert.Equal(expectedTimePeriod, result);
        }

        [Fact]
        public void Contructor_CorrectString_ShouldReturnTimePeriod() {
            // Arrange
            string formattedTime = "110:10:10";
            // Act
            var timePeriod = new TimePeriod(formattedTime);
            // Assert
            Assert.Equal(110, timePeriod.Hours);
            Assert.Equal(10, timePeriod.Minutes);
            Assert.Equal(10, timePeriod.Seconds);
        }

        [Fact]
        public void Contructor_WrongStringFormat_ShouldThrowArgumentExcpetion() {
            // Arrange
            string formattedTime = "10-10-10";
            // Act
            Action contructor = () => new TimePeriod(formattedTime);
            // Assert
            Assert.Throws<ArgumentException>(contructor);
        }

        [Fact]
        public void Contructor_EmptyString_ShouldThrowArgumentExcpetion() {
            // Arrange
            string emptyString = string.Empty;
            // Act
            Action contructor = () => new TimePeriod(emptyString);
            // Assert
            Assert.Throws<ArgumentException>(contructor);
        }

        [Fact]
        public void Contructor_StringWithTooLargeValues_ShouldThrowArgumentExcpetion() {
            // Arrange
            string stringWithTooLargeValues = "24:60:60";
            // Act
            Action contructor = () => new TimePeriod(stringWithTooLargeValues);
            // Assert
            Assert.Throws<ArgumentException>(contructor);
        }

        [Fact]
        public void Properties_Readonly_ShouldBeImmutable() {
            // Arrange
            var properties = typeof(TimePeriod).GetProperties();
            // Act
            // Assert
            Assert.True(properties.Length == 3);
            foreach(var property in properties) {
                Assert.False(property.CanWrite);
            }
        }

        [Fact]
        public void ToString_TimePeriod_ShouldReturnFormattedString() {
            // Arrange
            var timePeriod = new TimePeriod(100, 2, 3);
            // Act
            var result = timePeriod.ToString();
            // Assert
            Assert.Equal("100:02:03", result);
        }


        [Fact]
        public void Equals_SameTimePeriods_ShouldReturnTrue() {
            // Arrange
            var timePeriod = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 3);
            // Act
            // Assert
            Assert.True(timePeriod.Equals(otherTimePeriod));
        }

        [Fact]
        public void Equals_DiffrentTimePeriods_ShouldReturnFalse() {
            // Arrange
            var timePeriod = new TimePeriod(1, 2, 4);
            var otherTimePeriod = new TimePeriod(1, 2, 3);
            // Act
            // Assert
            Assert.False(timePeriod.Equals(otherTimePeriod));
        }

        [Fact]
        public void CompareTo_SameTimePeriod_ShouldReturnZero() {
            // Arrange
            var timePeriod = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 3);
            // Act
            var result = timePeriod.CompareTo(otherTimePeriod);
            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CompareTo_GreaterTimePeriod_ShouldReturnMinusOne() {
            // Arrange
            var timePeriod = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 4);
            // Act
            var result = timePeriod.CompareTo(otherTimePeriod);
            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void CompareTo_SmallerTimePeriod_ShouldReturnOne() {
            // Arrange
            var timePeriod = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 2);
            // Act
            var result = timePeriod.CompareTo(otherTimePeriod);
            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void EqualsOperator_SameTime_ShouldReturnTrue() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 3);
            // Act
            var result = time == otherTimePeriod;
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsOperator_DiffrentTimePeriod_ShouldReturnFalse() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(2, 3, 4);
            // Act
            var result = time == otherTimePeriod;
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void NotEqualsOperator_DiffrentTimePeriod_ShouldReturnTrue() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 4);
            // Act
            var result = time != otherTimePeriod;
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void NotEqualsOperator_SameTimePeriod_ShouldReturnFalse() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 3);
            // Act
            var result = time != otherTimePeriod;
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterOperator_SmallerTimePeriod_ShouldReturnTrue() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 2);
            // Act
            var result = time > otherTimePeriod;
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterOperator_GreaterTimePeriod_ShouldReturnFalse() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 4);
            // Act
            var result = time > otherTimePeriod;
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterOrEqualOperator_SmallerTimePeriod_ShouldReturnTrue() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 2);
            // Act
            var result = time >= otherTimePeriod;
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterOrEqualOperator_GreaterTimePeriod_ShouldReturnFalse() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 4);
            // Act
            var result = time >= otherTimePeriod;
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessOperator_SmallerTimePeriod_ShouldReturnFalse() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 2);
            // Act
            var result = time < otherTimePeriod;
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessOperator_GreaterTimePeriod_ShouldReturnTrue() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 4);
            // Act
            var result = time < otherTimePeriod;
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void LessOrEqualOperator_SmallerTimePeriod_ShouldReturnFalse() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 2);
            // Act
            var result = time <= otherTimePeriod;
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LessOrEqualOperator_GreaterTimePeriod_ShouldReturnTrue() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 4);
            // Act
            var result = time <= otherTimePeriod;
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void PlusOperator_SameTimePeriod_ShouldReturnDoubledTimePeriod() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 3);
            var expectedTimePeriod = new TimePeriod(2, 4, 6);
            // Act
            var result = time + otherTimePeriod;
            // Assert
            Assert.Equal(expectedTimePeriod, result);
        }

        [Fact]
        public void MinusOperator_SameTimePeriod_ShouldReturnTimePeriodZero() {
            // Arrange
            var time = new TimePeriod(1, 2, 3);
            var otherTimePeriod = new TimePeriod(1, 2, 3);
            var expectedTimePeriod = new TimePeriod(0, 0, 0);
            // Act
            var result = time - otherTimePeriod;
            // Assert
            Assert.Equal(expectedTimePeriod, result);
        }

        [Fact]
        public void MinusOperator_ToMuchTimePeriod_ShouldThrowOverflowException() {
            // Arrange
            var time = new TimePeriod(0, 0, 1);
            var otherTimePeriod = new TimePeriod(0, 0, 2);
            // Act
            Func<object> result = () => time - otherTimePeriod;
            // Assert
            Assert.Throws<OverflowException>(result);
        }
    }
}
