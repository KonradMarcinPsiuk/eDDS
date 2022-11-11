using DTOs;

using PlantModel.Queries;
using System.Net.Http.Json;

namespace LineAreaService;

public interface ILineAreaService
{
    Task<IEnumerable<LineAreaDto>> GetLineAreasForProductionLine(int lineId);
}

public class Service : ILineAreaService
{
    private readonly HttpClient _client;

    public Service(HttpClient client)
    {
        _client = client;
    }

    public Service(IHttpClientFactory factory)
    {
        _client = factory.CreateClient(ClientNames.Names.PlantClient);
    }

    public async Task<IEnumerable<LineAreaDto>> GetLineAreasForProductionLine(int lineId)
    {
        return await _client.GetFromJsonAsync<IEnumerable<LineAreaDto>>(Queries.GetLineAreasForLine(lineId)) ?? Array.Empty<LineAreaDto>();
    }
}