using System.Diagnostics;
using System.Net.Http.Json;

using PlantModel.Queries;
using DTOs;

namespace PlantService
{
    public interface IPlantService
    {
        Task<IEnumerable<PlantDto>> GetPlants();
    }

    public class Service : IPlantService
    {
        private readonly HttpClient _client;
        //private Type _type;

        public Service(HttpClient client)
        {
            _client = client;
            //_type = typeof(TPlant);
        }

        public Service(IHttpClientFactory factory)
        {
            _client = factory.CreateClient(ClientNames.Names.PlantClient);
            //_type = typeof(TPlant);
        }

        public async Task<IEnumerable<PlantDto>> GetPlants()
        {
           
                var plants= await _client.GetFromJsonAsync<IEnumerable<PlantDto>>(Queries.GetPlants());
                return plants;
            
        }
    }


}

