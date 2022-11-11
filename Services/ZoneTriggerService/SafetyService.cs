using System.Net.Http.Json;
using DTOs;
using ZoneTrigger.Queries;

namespace ZoneTriggerService;

public interface ISafetyService
{
    Task<SafetyZoneTriggerDto> GetSafetyZoneTrigger(int reportId);
    Task<SafetyZoneTriggerQuestionDto[]> GetSafetyZoneTriggerQuestions(int? deptId = null);
    Task<bool> SaveSafetyZoneTriggerQuestion(SafetyZoneTriggerQuestionDto question);
    Task<bool> SaveSafetyZoneTrigger(SafetyZoneTriggerDto trigger);
    Task<bool> AddDepartmentToQuestion(SafetyZoneTriggerQuestionDepartmentDto link);
    Task<bool> DeleteDepartmentToQuestion(SafetyZoneTriggerQuestionDepartmentDto link);
    Task<SafetyZoneTriggerQuestionDto> GetSafetyQuestion(int id);
}

public class SafetyService : ISafetyService
{
    private readonly HttpClient _client;
    public SafetyService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient(ClientNames.Names.ZoneTrigger);
    }

    public async Task<SafetyZoneTriggerDto> GetSafetyZoneTrigger(int reportId)
    {
        var report =
            await _client.GetFromJsonAsync<SafetyZoneTriggerDto>(
                SafetyZoneTriggerQueries.GetSafetyZoneTrigger(reportId));
        return report;
    }
//
    public async Task<SafetyZoneTriggerQuestionDto[]> GetSafetyZoneTriggerQuestions(int? deptId = null)
    {
        var questions =
            await _client.GetFromJsonAsync<SafetyZoneTriggerQuestionDto[]>(
                SafetyZoneTriggerQueries.GetSafetyZoneTriggerQuestion(deptId));
        return questions;
    }
//
    public async Task<bool> SaveSafetyZoneTriggerQuestion(SafetyZoneTriggerQuestionDto question)
    {
        var response = await _client.PostAsJsonAsync(SafetyZoneTriggerQueries.SaveSafetyZoneTriggerQuestion, question);
        return response.IsSuccessStatusCode;
    }
//
    public async Task<bool> SaveSafetyZoneTrigger(SafetyZoneTriggerDto trigger)
    {
        var response = await _client.PostAsJsonAsync(SafetyZoneTriggerQueries.SaveSafetyZoneTrigger, trigger);
        return response.IsSuccessStatusCode;
    }
//
    public async Task<bool> AddDepartmentToQuestion(SafetyZoneTriggerQuestionDepartmentDto link)
    {
        var response = await _client.PostAsJsonAsync(SafetyZoneTriggerQueries.AddDepartmentToQuestion, link);
        return response.IsSuccessStatusCode;
    }
    
    public async Task<bool> DeleteDepartmentToQuestion(SafetyZoneTriggerQuestionDepartmentDto link)
    {
        var check = await _client.PostAsJsonAsync(SafetyZoneTriggerQueries.DeleteDepartmentLink, link);
        return check.IsSuccessStatusCode;
    }

    public async Task<SafetyZoneTriggerQuestionDto> GetSafetyQuestion(int id)
    {
        var question =
            await _client.GetFromJsonAsync<SafetyZoneTriggerQuestionDto>(SafetyZoneTriggerQueries
                .GetSafetyQuestion(id));
        return question;
    }
}