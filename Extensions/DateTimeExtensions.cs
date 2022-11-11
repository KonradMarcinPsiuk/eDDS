namespace Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToApiFormat(this DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}
