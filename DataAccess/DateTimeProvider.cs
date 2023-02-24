using System;

namespace DataAccess
{
    public class DateTimeProvider : IDateTimeProvider
    {
        // Store the current time when this class is initiated. Instances are intended to be used transiently.
        private readonly DateTime _now = DateTime.UtcNow;
        public DateTime Now => _now;
    }
}