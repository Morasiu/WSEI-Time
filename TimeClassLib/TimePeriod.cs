using System;
using System.Collections.Generic;
using System.Text;

namespace TimeClassLib {
    /// <summary>
    /// Struct that represents time duration.
    /// </summary>
    public struct TimePeriod {

        #region Contructors

        /// <summary>
        /// Generates time period.
        /// </summary>
        /// <param name="hours">Hours, cannot be less than 0</param>
        /// <param name="minutes">Minutes, must be in range 0-59</param>
        /// <param name="seconds">Seconds, must be in range 0-59</param>
        public TimePeriod(uint hours, byte minutes, byte seconds) {
            if(IsParamLessThanZero(hours, minutes, seconds))
                throw new ArgumentException("Parameter was less than 0");
            if(minutes > 59) throw new ArgumentException("Minutes were more than 59");
            if(seconds > 59) throw new ArgumentException("Seconds were more than 59");

            _seconds = seconds + minutes * 60 + hours * 60 * 60;
        }

        public TimePeriod( byte minutes, byte seconds) : this(0, minutes, seconds) { }

        public TimePeriod(byte seconds) : this(0, 0, seconds) { }

        #endregion

        #region Properties

        public int Hours => (int)_seconds / 3600;
        public int Minutes => (int) _seconds % 3600 / 60;
        public int Seconds => (int) _seconds % 3600 % 60;

        #endregion

        #region Privates

        private readonly long _seconds;

        private static bool IsParamLessThanZero(uint hours, byte minutes, byte seconds) {
            return hours < 0 || minutes < 0 || seconds < 0;
        }
        #endregion
    }
}
