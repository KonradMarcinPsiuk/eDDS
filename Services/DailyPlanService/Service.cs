using DailyPlanner.Queries;
using DTOs;
using JsonHelpers;
using System.Net.Http.Json;
using System.Text.Json;

namespace DailyPlanService
{
    public interface IDailyPlanService
    {
        Task<IEnumerable<DailyPlanDto>> GetDailyPlans(int lineId, int week, int year);
        Task<DailyPlanDto?> GetDailyPlan(int planId);
        Task<bool> SavePlan(DailyPlanDto plan);
    }

    public class Service : IDailyPlanService
    {
        private readonly HttpClient _client;

        public Service(HttpClient client)
        {
            _client = client;
        }

        public Service(IHttpClientFactory factory)
        {
            _client = factory.CreateClient(ClientNames.Names.DailyPlanner);
        }

        public async Task<IEnumerable<DailyPlanDto>> GetDailyPlans(int lineId, int week, int year)
        {
            var result= await _client.GetStringAsync(Queries.GetPlans(lineId, year, week));
            var deserialized = JsonSerializer.Deserialize<IEnumerable<DailyPlanDto>>(result, JsonSerializerOptionsClass.JsonOptions());
            return deserialized ?? Array.Empty<DailyPlanDto>();

        }

        public async Task<DailyPlanDto?> GetDailyPlan(int planId)
        {
            var result = await _client.GetStringAsync(Queries.GetDailyPlan(planId));
            var deserialized = JsonSerializer.Deserialize<DailyPlanDto>(result, JsonSerializerOptionsClass.JsonOptions());
            return deserialized;

        }

        public async Task<bool> SavePlan(DailyPlanDto plan)
        {
            var serialized = JsonSerializer.Serialize(plan, JsonSerializerOptionsClass.JsonOptions());
            var result = await _client.PostAsJsonAsync(Queries.SavePlan(), plan,JsonSerializerOptionsClass.JsonOptions());
            return result.IsSuccessStatusCode;
        }
    }
}
