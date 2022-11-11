using System.Net.Http.Json;
using PlantModel.Queries;
using DTOs;

namespace ProductionLineService;

public interface IProductionLineService
{
    Task<IEnumerable<ProductionLineDto>> GetProductionLinesForDepartment(int deptId);
}

public class Service : IProductionLineService
{
    private readonly HttpClient _client;

    public Service(HttpClient client)
    {
        _client = client;
    }
    
    public Service(IHttpClientFactory factory)
    {
        _client = factory.CreateClient(ClientNames.Names.PlantClient);
        //_type = typeof(TPlant);
    }

    public async Task<IEnumerable<ProductionLineDto>> GetProductionLinesForDepartment(int deptId)
    {
        return await _client.GetFromJsonAsync<IEnumerable<ProductionLineDto>>(Queries.GetLinesForDepartment(deptId)) ?? Array.Empty<ProductionLineDto>();
    }
}