using System;

namespace Utils
{
    public static class DateTimeUtils
    {
        public static DateTime FromUnixTimeStamp(long unixTimeStamp)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(Convert.ToDouble(unixTimeStamp));
        }
    }
}
