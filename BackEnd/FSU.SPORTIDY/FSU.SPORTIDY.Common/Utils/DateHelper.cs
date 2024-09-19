namespace FSU.SPORTIDY.Repository.Utils
{
    public class DateHelper
    {
        public static DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval  = dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }

        public static bool ValidateDates(DateTime? dateStart, DateTime? dateEnd)
        {
            // Check if DateStart is null or empty
            if (dateStart == null || dateEnd == null || !dateStart.HasValue || !dateEnd.HasValue)
            {
                return false;
            }

            // Check if DateStart is in the past
            if (dateStart.Value <= DateTime.Now)
            {
                return false;
            }
           
            // Check if DateStart is before DateEnd
            if (dateStart >= dateEnd)
            {
                return false;
            }
            // If all checks pass, the dates are valid
            return true;
        }

    }
}
