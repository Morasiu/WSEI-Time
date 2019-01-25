using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeClassLib {
    /// <summary>
    /// Struct that represents time duration.
    /// </summary>
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod> {

        #region Contructors

        /// <summary>
        /// Generates time period.
        /// </summary>
        /// <param name="hours">Hours, cannot be less than 0</param>
        /// <param name="minutes">Minutes, must be in range 0-59</param>
        /// <param name="seconds">Seconds, must be in range 0-59</param>
        public TimePeriod(uint hours, byte minutes, byte seconds) {
            CheckIfParamsArelessThanZero(hours, minutes, seconds);
            CheckIfMinutesAreInRange(minutes);
            CheckIfSecondsAreInRange(seconds);

            _seconds = seconds + minutes * 60 + hours * 60 * 60;
        }

        /// <summary>
        /// Generates time period.
        /// </summary>
        /// <param name="minutes">Minutes, must be in range 0-59</param>
        /// <param name="seconds">Seconds, must be in range 0-59</param>
        public TimePeriod( byte minutes, byte seconds) : this(0, minutes, seconds) { }

        /// <summary>
        /// Generates time period.
        /// </summary>
        /// <param name="seconds">Seconds, must be in range 0-59</param>
        public TimePeriod(byte seconds) : this(0, 0, seconds) { }

        /// <summary>
        /// Generates time period based on diffrence in two Times
        /// </summary>
        /// <param name="firstTime">First time</param>
        /// <param name="secondTime">Seconds time</param>
        public TimePeriod(Time firstTime, Time secondTime) : this(
            GetHoursDiffrence(firstTime, secondTime), 
            GetMinutesDiffrence(firstTime, secondTime), 
            GetSecondsDiffrence(firstTime, secondTime)) { }

        /// <summary>
        /// Generates time period from string (hhhh:mm:ss)
        /// </summary>
        /// <param name="formattedTime">String in format hhhh:mm:ss</param>
        public TimePeriod(string formattedTime) {
            var timeStrings = formattedTime.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            CheckTimeFormat(timeStrings);
            uint hours; byte minutes; byte seconds;
            try {
                hours = Convert.ToUInt32(timeStrings[0]);
                minutes = Convert.ToByte(timeStrings[1]);
                seconds = Convert.ToByte(timeStrings[2]);
            } catch (Exception) {
                throw new ArgumentException("Wrong format");
            }

            CheckIfParamsArelessThanZero(hours, minutes, seconds);
            CheckIfMinutesAreInRange(minutes);
            CheckIfSecondsAreInRange(seconds);
            _seconds = hours * 60 * 60 + minutes * 60 + seconds;
        } 

        #endregion

        #region Properties

        /// <summary>
        /// Hours. Range 0-23
        /// </summary>
        public int Hours => (int)_seconds / 3600;
        /// <summary>
        /// Minutes. Range 0-59
        /// </summary>
        public int Minutes => (int) _seconds % 3600 / 60;
        /// <summary>
        /// Seconds. Range 0-59
        /// </summary>
        public int Seconds => (int) _seconds % 3600 % 60;

        #endregion

        #region Methods

        public override string ToString() => 
            $"{ConvertToTimeFormatValue((uint) Hours)}:" +
            $"{ConvertToTimeFormatValue((uint) Minutes)}:" +
            $"{ConvertToTimeFormatValue((uint) Seconds)}";

        public bool Equals(TimePeriod other) => _seconds == other._seconds;

        public int CompareTo(TimePeriod other) =>_seconds.CompareTo(other._seconds);

        #endregion

        #region Operators

        public static bool operator ==(TimePeriod timePeriod, TimePeriod other) => timePeriod.Equals(other);
        public static bool operator !=(TimePeriod timePeriod, TimePeriod other) => !timePeriod.Equals(other);
        public static bool operator <(TimePeriod time1, TimePeriod time2) => time1.CompareTo(time2) < 0;
        public static bool operator >(TimePeriod time1, TimePeriod time2) => time1.CompareTo(time2) > 0;
        public static bool operator <=(TimePeriod time1, TimePeriod time2) => time1.CompareTo(time2) <= 0;
        public static bool operator >=(TimePeriod time1, TimePeriod time2) => time1.CompareTo(time2) >= 0;
        public static TimePeriod operator +(TimePeriod time1, TimePeriod time2) {
            int seconds = 0, minutes = 0, hours = 0;
            seconds = time1.Seconds + time2.Seconds;
            if(seconds > 59) {
                minutes = seconds / 60;
                seconds = seconds % 60;
            }
            minutes += time1.Minutes + time2.Minutes;
            if(minutes > 59) {
                hours = minutes / 60;
                minutes = minutes % 60;
            }
            hours += time1.Hours + time2.Hours;
            return new TimePeriod((uint)hours, (byte)minutes, (byte)seconds);
        }
        public static TimePeriod operator -(TimePeriod time1, TimePeriod time2) {
            int seconds = 0, minutes = 0, hours = 0;
            seconds = time1.Seconds - time2.Seconds;
            if(seconds < 0) {
                minutes -= (seconds / 60) + 1;
                seconds = 60 - Math.Abs(seconds);
            }
            minutes += time1.Minutes - time2.Minutes;
            if(minutes < 0) {
                hours -= (minutes / 60) + 1;
                minutes = 60 - Math.Abs(minutes);
            }
            hours += time1.Hours - time2.Hours;
            if(hours < 0) throw new OverflowException("Time was less than 00:00:00");
            return new TimePeriod((uint) hours, (byte)minutes, (byte)seconds);
        }
        #endregion

        #region Privates

        private readonly long _seconds;

        private static bool IsAnyParamLessThanZero(uint hours, byte minutes, byte seconds) {
            return hours < 0 || minutes < 0 || seconds < 0;
        }
        private static uint GetHoursDiffrence(Time firstTime, Time secondTime) {
            Time timeDiffrence = GetTimeDiffrence(firstTime, secondTime);
            return timeDiffrence.Hours;
        }
        private static byte GetMinutesDiffrence(Time firstTime, Time secondTime) {
            Time timeDiffrence = GetTimeDiffrence(firstTime, secondTime);
            return timeDiffrence.Minutes;
        }
        private static byte GetSecondsDiffrence(Time firstTime, Time secondTime) {
            Time timeDiffrence = GetTimeDiffrence(firstTime, secondTime);
            return timeDiffrence.Seconds;
        }
        private static Time GetTimeDiffrence(Time firstTime, Time secondTime) {
            Time timeDiffrence;
            if(firstTime >= secondTime) {
                timeDiffrence = firstTime - secondTime;
            } else {
                timeDiffrence = secondTime - firstTime;
            }

            return timeDiffrence;
        }

        private static void CheckIfSecondsAreInRange(byte seconds) {
            if(seconds > 59) throw new ArgumentException("Seconds were more than 59");
        }

        private static void CheckIfMinutesAreInRange(byte minutes) {
            if(minutes > 59) throw new ArgumentException("Minutes were more than 59");
        }

        private static void CheckIfParamsArelessThanZero(uint hours, byte minutes, byte seconds) {
            if(IsAnyParamLessThanZero(hours, minutes, seconds))
                throw new ArgumentException("Parameter was less than 0");
        }

        private static void CheckTimeFormat(string[] timeStrings) {
            if(timeStrings.Length != 3 || !ConatinsDigits(timeStrings)) throw new ArgumentException("Wrong format");
        }

        private static bool ConatinsDigits(string[] timeStrings) => timeStrings.All(time => time.All(c => char.IsDigit(c)));
        private static string ConvertToTimeFormatValue(uint value) => value < 10 ? "0" + value.ToString() : value.ToString();

        #endregion
    }
}
