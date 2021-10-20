using System;

namespace IsuExtra
{
    public class TimeInterval
    {
        private DateTime _startTime;
        private DateTime _finishTime;

        public TimeInterval(DateTime startTime, DateTime finishTime)
        {
            _startTime = startTime;
            _finishTime = finishTime;
        }

        public bool IsCrossed(TimeInterval otherTime)
        {
            return (otherTime._startTime >= _startTime && otherTime._startTime <= _finishTime) ||
                   (otherTime._finishTime >= _startTime && otherTime._finishTime <= _finishTime);
        }
    }
}