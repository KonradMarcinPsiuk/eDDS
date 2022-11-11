namespace ClientNames
{
    public static class Addresses
    {
        public static Uri PlantApiLocal => new ("http://localhost:5055");
        public static Uri PeopleApiLocal => new Uri("http://localhost:5128");
        public static Uri DefectApiLocal => new Uri("http://localhost:5260");
        public static Uri DailyPlannerLocal = new Uri("http://localhost:5243");
        public static Uri PlannedStopLocal = new Uri("http://localhost:5246");
    }
}
