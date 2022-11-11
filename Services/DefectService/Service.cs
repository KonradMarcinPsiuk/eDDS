using Defects.Queries;
using DTOs;
using System.Net.Http.Json;

namespace DefectService;

public interface IDefectService
{
    Task<IEnumerable<DefectDto>> GetOpenDefectsForProductionLine(int lineId);
    Task<IEnumerable<DefectDto>> GetDefectsForProductionLine(int lineId, string status);
    Task<bool> SaveDefect(DefectDto defect);
    Task<DefectDto?> GetDefect(Guid id);
    Task DeleteDefect(Guid defectId);
}

public class Service : IDefectService
{
    private readonly HttpClient _client;

    public Service(HttpClient client)
    {
        _client = client;
    }

    public Service(IHttpClientFactory factory)
    {
        _client = factory.CreateClient(ClientNames.Names.DefectClient);
    }

    public async Task<IEnumerable<DefectDto>> GetOpenDefectsForProductionLine(int lineId)
    {
        return await GetDefectsForProductionLine(lineId, "open");
    }

    public async Task<IEnumerable<DefectDto>> GetDefectsForProductionLine(int lineId, string status)
    {
        return await _client.GetFromJsonAsync<IEnumerable<DefectDto>>(Queries.GetDefectsForLine(lineId, status)) ?? Array.Empty<DefectDto>();
    }

    public async Task<bool> SaveDefect(DefectDto defect)
    {
        var check = await _client.PostAsJsonAsync(Queries.SaveDefect(), defect);
        return check.IsSuccessStatusCode;
    }

    public async Task<DefectDto?> GetDefect(Guid id)
    {
        return await _client.GetFromJsonAsync<DefectDto>(Queries.GetDefect(id));
    }

    public async Task DeleteDefect(Guid defectId)
    {
        await _client.DeleteAsync(Queries.DeleteDefect(defectId));
    }
    
}