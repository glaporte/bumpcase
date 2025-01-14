namespace calendar.Utilities
{
    public class DateUtility
    {
        public static DateTime CleanMeetingDate(DateTime inputDate)
        {
            return new DateTime(inputDate.Year,
                inputDate.Month,
                inputDate.Day,
                inputDate.Hour,
                0,
                0);
        }
    }
}
