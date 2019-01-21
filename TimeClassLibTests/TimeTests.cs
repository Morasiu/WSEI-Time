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
            byte tooManyMinutes = 60;
            // Act
            Func<object> conctructor = () => new Time(hours, tooManyMinutes);
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

        [Fact]
        public void ToString_Time_ShouldReturnFormattedString() {
            // Arrange
            var time = new Time(1, 2, 3);
            // Act
            var result = time.ToString();
            // Assert
            Assert.Equal("01:02:03", result);
        }

        [Fact]
        public void Equals_SameTime_ShouldReturnTrue() {
            // Arrange
            var time = new Time(1, 2, 3);
            var otherTime = new Time(1, 2, 3);
            // Act
            // Assert
            Assert.True(time.Equals(otherTime));
        }

        [Fact]
        public void Equals_DiffrentTime_ShouldReturnFalse() {
            // Arrange
            var time = new Time(1, 2, 3);
            var otherTime = new Time(1, 2, 4);
            // Act
            // Assert
            Assert.False(time.Equals(otherTime));
        }

        [Fact]
        public void CompareTo_SameTime_ShouldReturnZero() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 3);
			// Act
			var result = time.CompareTo(otherTime);
			// Assert
			Assert.Equal(0, result);
		}

		[Fact]
		public void CompareTo_GreaterTime_ShouldReturnMinusOne() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 4);
			// Act
			var result = time.CompareTo(otherTime);
			// Assert
			Assert.Equal(-1, result);
		}

		[Fact]
		public void CompareTo_SmallerTime_ShouldReturnOne() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 2);
			// Act
			var result = time.CompareTo(otherTime);
			// Assert
			Assert.Equal(1, result);
		}

		[Fact]
		public void EqualsOperator_SameTime_ShouldReturnTrue() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 3);
			// Act
			var result = time == otherTime;
			// Assert
			Assert.True(result);
		}

		[Fact]
		public void EqualsOperator_DiffrentTime_ShouldReturnFalse() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(2, 3, 4);
			// Act
			var result = time == otherTime;
			// Assert
			Assert.False(result);
		}

		[Fact]
		public void NotEqualsOperator_DiffrentTime_ShouldReturnTrue() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 4);
			// Act
			var result = time != otherTime;
			// Assert
			Assert.True(result);
		}

		[Fact]
		public void NotEqualsOperator_SameTime_ShouldReturnFalse() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 3);
			// Act
			var result = time != otherTime;
			// Assert
			Assert.False(result);
		}

		[Fact]
		public void GreaterOperator_SmallerTime_ShouldReturnTrue() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 2);
			// Act
			var result = time > otherTime;
			// Assert
			Assert.True(result);
		}

		[Fact]
		public void GreaterOperator_GreaterTime_ShouldReturnFalse() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 4);
			// Act
			var result = time > otherTime;
			// Assert
			Assert.False(result);
		}

		[Fact]
		public void GreaterOrEqualOperator_SmallerTime_ShouldReturnTrue() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 2);
			// Act
			var result = time >= otherTime;
			// Assert
			Assert.True(result);
		}

		[Fact]
		public void GreaterOrEqualOperator_GreaterTime_ShouldReturnFalse() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 4);
			// Act
			var result = time >= otherTime;
			// Assert
			Assert.False(result);
		}

		[Fact]
		public void LessOperator_SmallerTime_ShouldReturnFalse() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 2);
			// Act
			var result = time < otherTime;
			// Assert
			Assert.False(result);
		}

		[Fact]
		public void LessOperator_GreaterTime_ShouldReturnTrue() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 4);
			// Act
			var result = time < otherTime;
			// Assert
			Assert.True(result);
		}

		[Fact]
		public void LessOrEqualOperator_SmallerTime_ShouldReturnFalse() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 2);
			// Act
			var result = time <= otherTime;
			// Assert
			Assert.False(result);
		}

		[Fact]
		public void LessOrEqualOperator_GreaterTime_ShouldReturnTrue() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 4);
			// Act
			var result = time <= otherTime;
			// Assert
			Assert.True(result);
		}

		[Fact]
		public void PlusOperator_SameTime_ShouldReturnDoubledTime() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 3);
			var expectedTime = new Time(2, 4, 6);
			// Act
			var result = time + otherTime;
			// Assert
			Assert.Equal(expectedTime, result);
		}

		[Fact]
		public void PlusOperator_ToMuchTime_ShouldThrowOverflowException() {
			// Arrange
			var time = new Time(23, 59, 59);
			var otherTime = new Time(0, 0, 1);
			// Act
			Func<object> result = () => time + otherTime ;
			// Assert
			Assert.Throws<OverflowException>(result);
		}

		[Fact]
		public void MinusOperator_SameTime_ShouldReturnTimeZero() {
			// Arrange
			var time = new Time(1, 2, 3);
			var otherTime = new Time(1, 2, 3);
			var expectedTime = new Time(0, 0, 0);
			// Act
			var result = time - otherTime;
			// Assert
			Assert.Equal(expectedTime, result);
		}

		[Fact]
		public void MinusOperator_ToMuchTime_ShouldThrowOverflowException() {
			// Arrange
			var time = new Time(0, 0, 1);
			var otherTime = new Time(0, 0, 2);
			// Act
			Func<object> result = () => time - otherTime;
			// Assert
			Assert.Throws<OverflowException>(result);
		}
	}
}
