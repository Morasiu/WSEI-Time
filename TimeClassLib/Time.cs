﻿using System;
using System.Text.RegularExpressions;

namespace TimeClassLib {
    /// <summary>
    /// Struct that represnts time value in format hh:mm:ss
    /// </summary>
    public struct Time : IEquatable<Time>, IComparable<Time> {

        #region Properties

        /// <summary>
        /// Hours. Range 0-23
        /// </summary>
        public byte Hours => _hours;
        /// <summary>
        /// Minutes. Range 0-59
        /// </summary>
        public byte Minutes => _minutes;
        /// <summary>
        /// Seconds. Range 0-59
        /// </summary>
        public byte Seconds => _seconds;

        #endregion

        #region Contructors

        /// <summary>
        /// Creates time value.
        /// </summary>
        /// <param name="hours">Hours, cannot be more that 23</param>
        /// <param name="minutes">Minutes, cannot be more that 59</param>
        /// <param name="seconds">Seconds, cannot be more that 59</param>
        public Time(byte hours, byte minutes, byte seconds) {
            if(hours > 23) throw new ArgumentException("Hours were more than 23");
            _hours = hours;
            if(minutes > 59) throw new ArgumentException("Minutes were more than 59");
            _minutes = minutes;
            if(seconds > 59) throw new ArgumentException("Seconds were more than 59");
            _seconds = seconds;
        }

        /// <summary>
        /// Create time value, with given hours, minutes 0, seconds 0.
        /// </summary>
        /// <param name="hours">Hours, cannot be more that 23</param>
        public Time(byte hours) : this(hours, 0, 0) { }

        /// <summary>
        /// Create time value, with given hours, minutes, seconds 0.
        /// </summary>
        /// <param name="hours">Hours, cannot be more that 23</param>
        /// <param name="minutes">Minutes, cannot be more that 59</param>
        public Time(byte hours, byte minutes) : this(hours, minutes, 0) { }

        /// <summary>
        /// Create time value from string. String must be in format hh:mm:ss
        /// </summary>
        /// <param name="time">Time in string. Must be in format hh:mm:ss</param>
        public Time(string time) : this(GetHours(time), GetMinutes(time), GetSeconds(time)) {
            if(IsTimeWrongFormat(time)) throw new ArgumentException("Wrong time format");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Converts Time to string value.
        /// </summary>
        /// <returns>Time in format hh:mm;ss</returns>
        public override string ToString() => $"{ConvertToTimeFormatValue(_hours)}:{ConvertToTimeFormatValue(_minutes)}:{ConvertToTimeFormatValue(_seconds)}";

        /// <summary>
        /// Compares two Times
        /// </summary>
        /// <param name="other">Second time to compare</param>
        /// <returns>True, if values (hours, minutes, seconds) of both Time are equals</returns>
        public bool Equals(Time other) {
            return _hours == other._hours
                && _minutes == other._minutes
                && _seconds == other._seconds;
        }

        /// <summary>
        /// Compares two Times
        /// </summary>
        /// <param name="other">Second time to compare</param>
        /// <returns>Same as int comparing</returns>
        public int CompareTo(Time other) {
            if (Hours == other.Hours) 
                if(Minutes == other.Minutes)
                    return Seconds.CompareTo(other.Seconds);
                else
                    return Minutes.CompareTo(other.Minutes);
            else 
                return Hours.CompareTo(other.Hours);
        }
        
        #endregion

        #region Privates

        private readonly byte _hours;
        private readonly byte _minutes;
        private readonly byte _seconds;

        private static string ConvertToTimeFormatValue(byte value) => value < 10 ? "0" + value.ToString() : value.ToString();
        private static bool IsTimeWrongFormat(string time) {
            return IsTimeWrongSize(time) || !Regex.IsMatch(time, @"(\d\d:\d\d:\d\d)");
        }
        private static bool IsTimeWrongSize(string time) => time.Length != 8;
        private static byte GetHours(string time) {
            if(IsTimeWrongSize(time)) throw new ArgumentException("Wrong string size");
            return Convert.ToByte(time.Substring(0, 2));
        }
        private static byte GetMinutes(string time) {
            if(IsTimeWrongSize(time)) throw new ArgumentException("Wrong string size");
            return Convert.ToByte(time.Substring(3, 2));
        }
        private static byte GetSeconds(string time) {
            if(IsTimeWrongSize(time)) throw new ArgumentException("Wrong string size");
            return Convert.ToByte(time.Substring(6, 2));
        }

		#endregion

		#region Operators

		public static bool operator ==(Time time1, Time time2) => time1.Equals(time2);
		public static bool operator !=(Time time1, Time time2) => !time1.Equals(time2);
		public static bool operator <(Time time1, Time time2) => time1.CompareTo(time2) < 0;
		public static bool operator >(Time time1, Time time2) => time1.CompareTo(time2) > 0;
		public static bool operator <=(Time time1, Time time2) => time1.CompareTo(time2) <= 0;
		public static bool operator >=(Time time1, Time time2) => time1.CompareTo(time2) >= 0;
		public static Time operator +(Time time1, Time time2) {
			int seconds = 0, minutes = 0, hours = 0;
			seconds = time1.Seconds + time2.Seconds;
			if (seconds > 59) {
				minutes = seconds / 60;
				seconds = seconds % 60;
			}
			minutes += time1.Minutes + time2.Minutes;
			if (minutes > 59) {
				hours = minutes / 60;
				minutes = minutes % 60;
			}
			hours += time1.Hours + time2.Hours;
			if (hours > 23) throw new OverflowException("Hours greater than 23");
			return new Time((byte) hours, (byte) minutes, (byte) seconds);
		}
		public static Time operator -(Time time1, Time time2) {
			int seconds = 0, minutes = 0, hours = 0;
			seconds = time1.Seconds - time2.Seconds;
			if (seconds < 0) {
				minutes -= (seconds / 60) + 1;
				seconds = 60 - Math.Abs(seconds);
			}
			minutes += time1.Minutes - time2.Minutes;
			if (minutes < 0) {
				hours -= (minutes / 60) + 1;
				minutes = 60 - Math.Abs(minutes);
			}
			hours += time1.Hours - time2.Hours;
			if (hours < 0) throw new OverflowException("Time was less than 00:00:00");
			return new Time((byte)hours, (byte)minutes, (byte)seconds);
		}
		#endregion
	}
}

