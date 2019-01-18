using System;

namespace TimeClassLib {
    public struct Time {
        public byte Hours => _hours;
        public byte Minutes => _minutes;
        public byte Seconds => _seconds;

        public Time(byte hours, byte minutes, byte seconds) {
            _hours = hours;
            _minutes = minutes;
            _seconds = seconds;
        }

        private readonly byte _hours;
        private readonly byte _minutes;
        private readonly byte _seconds;
    }
}
