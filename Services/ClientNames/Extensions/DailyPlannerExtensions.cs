using Microsoft.Extensions.DependencyInjection;

namespace ClientNames.Extensions
{
    public static class DailyPlannerExtensions
    {
        public static void AddDailyPlannerHttpClient(this IServiceCollection collection, string connectionString)
        {
            collection.AddHttpClient(ClientNames.Names.DailyPlanner, hp => hp.BaseAddress = new Uri(connectionString));
        }

    }
}
