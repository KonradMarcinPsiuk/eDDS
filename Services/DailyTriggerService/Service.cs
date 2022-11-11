using System.Net.Http.Json;
using ClientNames;
using DailyTrigger.Queries;
using DTOs;

namespace DailyTriggerService;

public interface IDailyTriggerService
{
    Task<IEnumerable<DailyTriggerQuestionDto>> GetAllQuestions();
    Task<bool> SaveProductionLineDailyTriggerLine(ProductionLineDailyTriggerQuestionDto link);
    Task<bool> DeleteProductionLineDailyTriggerLine(ProductionLineDailyTriggerQuestionDto link);
    Task<DailyTriggerQuestionDto?> GetQuestion(int questionId);
    Task<bool> SaveQuestion(DailyTriggerQuestionDto question);
    Task<int> GetNumberOfQuestionsAssignedToTheLine(int lineId);
    Task<bool> CheckIfAnswersForLineAndDateExists(DateOnly date, int lineId);
    Task<IEnumerable<DailyTriggerAnswerDto>?> GetTriggersForLineAndDate(DateOnly date, int lineId);
    Task SaveAnswers(IEnumerable<DailyTriggerAnswerDto> answers, CancellationToken token);
    Task<IEnumerable<DailyTriggerAnswerDto>> GetAnswersForDateAndDepartment(DateOnly date, int deptId);
}

public class Service : IDailyTriggerService
{
    private readonly HttpClient _client;

    public Service(IHttpClientFactory factory)
    {
        _client = factory.CreateClient(Names.DailyTrigger);
    }

    public async Task<IEnumerable<DailyTriggerQuestionDto>> GetAllQuestions()
    {
        return await _client.GetFromJsonAsync<IEnumerable<DailyTriggerQuestionDto>>(Queries.GetQuestions) ??
               new List<DailyTriggerQuestionDto>();
    }

    public async Task<bool> SaveProductionLineDailyTriggerLine(ProductionLineDailyTriggerQuestionDto link)
    {
        var check = await _client.PostAsJsonAsync(Queries.SaveProductionLineDailyTriggerLink, link);
        return check.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProductionLineDailyTriggerLine(ProductionLineDailyTriggerQuestionDto link)
    {
        var check = await _client.PostAsJsonAsync(Queries.DeleteProductionLineDailyTriggerLink, link);
        return check.IsSuccessStatusCode;
    }

    public async Task<DailyTriggerQuestionDto?> GetQuestion(int questionId)
    {
        return await _client.GetFromJsonAsync<DailyTriggerQuestionDto>(Queries.GetQuestion(questionId));
    }

    public async Task<bool> SaveQuestion(DailyTriggerQuestionDto question)
    {
        var check = await _client.PostAsJsonAsync(Queries.SaveQuestion, question);
        return check.IsSuccessStatusCode;
    }

    public async Task<int> GetNumberOfQuestionsAssignedToTheLine(int lineId)
    {
        return await _client.GetFromJsonAsync<int>(Queries.GetNumberOfQuestionsAssignedToTheLine(lineId));
    }

    public async Task<bool> CheckIfAnswersForLineAndDateExists(DateOnly date, int lineId)
    {
       
            var d = new DateTime(date.Year, date.Month, date.Day);
            var query = Queries.CheckIfAnswersForLineAndDateExists(d, lineId);
            return await _client.GetFromJsonAsync<bool>(query);
    }

    public async Task<IEnumerable<DailyTriggerAnswerDto>?> GetTriggersForLineAndDate(DateOnly date, int lineId)
    {
        return await _client.GetFromJsonAsync<IEnumerable<DailyTriggerAnswerDto>>(
            Queries.GetTriggersForLineAndDate(lineId, new DateTime(date.Year,date.Month,date.Day)));
    }

    public async Task SaveAnswers(IEnumerable<DailyTriggerAnswerDto> answers, CancellationToken token)
    {
        await _client.PostAsJsonAsync(Queries.SaveAnswers, answers, token);
    }

    public async Task<IEnumerable<DailyTriggerAnswerDto>> GetAnswersForDateAndDepartment(DateOnly date, int deptId)
    {
        return await _client.GetFromJsonAsync<IEnumerable<DailyTriggerAnswerDto>>(
            Queries.GetAnswersForDepartmentAndDate(new DateTime(date.Year,date.Month,date.Day), deptId));
    }
}