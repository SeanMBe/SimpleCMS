using System;

namespace SimpleCMS.Helpers
{
    public static class DateTimeHelpers
    {
        public static string FriendlyDate(this DateTime date)
        {
            return date.ToShortTimeString();
        }
    }
}