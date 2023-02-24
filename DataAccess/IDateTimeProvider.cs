using System;

namespace DataAccess
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}