using System.Net.Http.Json;
using ClientNames;
using DailyResults.Queries;
using DTOs;

namespace DailyResultsService;

public interface IDailyResultsService
{
    Task<DailyResultsDto?> GetResultsForDateAndLine(DateOnly date, int lineId);
    Task<IEnumerable<DailyResultsDto>> GetResultsForDateAndDepartment(DateOnly date, int deptId);
    Task<bool> CheckIfTheResultsForDateAndLine(DateOnly date, int lineId);
    Task<bool> SaveDailyResult(DailyResultsDto result, CancellationToken token);
}

public class Service : IDailyResultsService
{
    private readonly HttpClient _client;

    public Service(IHttpClientFactory factory)
    {
        _client = factory.CreateClient(Names.DailyResults);
    }

    public async Task<DailyResultsDto?> GetResultsForDateAndLine(DateOnly date, int lineId)
    {
        var result =
            await _client.GetFromJsonAsync<DailyResultsDto>(Queries.GetResultForLineAndDate(new DateTime(date.Year, date.Month, date.Day), lineId));
        return result;

    }

    public async Task<IEnumerable<DailyResultsDto>> GetResultsForDateAndDepartment(DateOnly date, int deptId)
    {
        var result = await _client.GetFromJsonAsync<IEnumerable<DailyResultsDto>>(Queries.GetResultsForDateAndDepartment(new DateTime(date.Year, date.Month, date.Day), deptId));
        return result ?? Array.Empty<DailyResultsDto>();
    }

    public async Task<bool> CheckIfTheResultsForDateAndLine(DateOnly date, int lineId)
    {
        var result = await _client.GetFromJsonAsync<bool>(Queries.CheckIfResultForLineAndDateExists(new DateTime(date.Year,date.Month,date.Day), lineId));
        return result;
    }

    public async Task<bool> SaveDailyResult(DailyResultsDto result, CancellationToken token)
    {
        try
        {
            await _client.PostAsJsonAsync(Queries.SaveDailyResult, result,token);
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }
}