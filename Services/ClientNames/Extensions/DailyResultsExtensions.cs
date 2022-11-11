using Microsoft.Extensions.DependencyInjection;

namespace ClientNames.Extensions;


    public static class DailyResultsExtensions
    {
        public static void AddDailyResultsHttpClient(this IServiceCollection collection, string connectionString)
        {
            collection.AddHttpClient(ClientNames.Names.DailyResults, hp => hp.BaseAddress = new Uri(connectionString));
        }

    }
