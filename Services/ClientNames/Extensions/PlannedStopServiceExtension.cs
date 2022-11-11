using Microsoft.Extensions.DependencyInjection;

namespace ClientNames.Extensions
{
    public static class PlannedStopServiceExtension
    {
        public static void AddPlannedStopHttpClient(this IServiceCollection collection, string connectionString)
        {
            collection.AddHttpClient(ClientNames.Names.PlannedStop, hp => hp.BaseAddress = new Uri(connectionString));
        }
    }
}
