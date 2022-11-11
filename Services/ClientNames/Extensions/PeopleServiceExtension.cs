using Microsoft.Extensions.DependencyInjection;

namespace ClientNames.Extensions
{
    public static class PeopleServiceExtension
    {
        public static void AddPeopleHttpClient(this IServiceCollection collection, string connectionString)
        {
            collection.AddHttpClient(ClientNames.Names.PeopleClient, hp => hp.BaseAddress = new Uri(connectionString));

        }
    }
}
