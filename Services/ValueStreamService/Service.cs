
using PlantModel.Queries;
using System.Net.Http.Json;
using DTOs;

namespace ValueStreamService
{
    public interface IValueStreamService
    {
        Task<IEnumerable<ValueStreamDto>> GetValueStreams(int plantId);
    }

    public class Service : IValueStreamService
    {
        private readonly HttpClient _client;

        public Service(IHttpClientFactory factory)
        {
            _client = factory.CreateClient(ClientNames.Names.PlantClient);
        }

        public Service(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ValueStreamDto>> GetValueStreams(int plantId)
        {
            return await _client.GetFromJsonAsync<IEnumerable<ValueStreamDto>>(Queries.GetValueStreamsForPlant(plantId)) ?? Array.Empty<ValueStreamDto>();
        }
    }

}
