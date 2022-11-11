using Microsoft.Extensions.DependencyInjection;

namespace ClientNames.Extensions;


    public static class DefectServiceExtension
    {
        public static void AddDefectHttpClient(this IServiceCollection collection, string connectionString)
        {
            collection.AddHttpClient(ClientNames.Names.DefectClient, hp => hp.BaseAddress = new Uri(connectionString));
        }

    }
