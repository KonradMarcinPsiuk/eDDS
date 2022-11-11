using Microsoft.Extensions.DependencyInjection;

namespace ClientNames.Extensions;


    public static class ZoneTriggerExtension
    {
        public static void AddZoneTriggerHttpClient(this IServiceCollection collection, string connectionString)
        {
            collection.AddHttpClient(ClientNames.Names.ZoneTrigger, hp => hp.BaseAddress = new Uri(connectionString));
        }

    }
