using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;


namespace ClientNames.Extensions
{
    public static class PlantServiceExtension
    {
        public static void AddPlantHttpClient(this IServiceCollection collection, string connectionString)
        {
            collection.AddHttpClient(ClientNames.Names.PlantClient, hp => hp.BaseAddress = new Uri(connectionString));
        }

    }
}
