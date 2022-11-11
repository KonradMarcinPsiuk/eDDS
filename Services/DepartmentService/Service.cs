
using PlantModel.Queries;
using System.Net.Http.Json;
using DTOs;

namespace DepartmentService
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetDepartmentsForValueStream(int vsId);
        Task<IEnumerable<DepartmentDto>> GetDepartmentsForPlant(int plantId);
    }

    public class Service : IDepartmentService
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

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsForValueStream(int vsId)
        {
            return await _client.GetFromJsonAsync<IEnumerable<DepartmentDto>>(Queries.GetDepartmentsForValueStream(vsId)) ?? Array.Empty<DepartmentDto>();
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsForPlant(int plantId)
        {
            return await _client.GetFromJsonAsync<IEnumerable<DepartmentDto>>(Queries.GetDepartmentsForPlant(plantId)) ?? Array.Empty<DepartmentDto>();
        }
    }
}
