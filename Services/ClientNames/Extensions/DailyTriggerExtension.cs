using Microsoft.Extensions.DependencyInjection;

namespace ClientNames.Extensions
{
    public static class DailyTriggerExtension
    {
        public static void AddDailyTriggerHttpClient(this IServiceCollection collection, string connectionString)
        {
            collection.AddHttpClient(ClientNames.Names.DailyTrigger, hp => hp.BaseAddress = new Uri(connectionString));
        }
    }
}
