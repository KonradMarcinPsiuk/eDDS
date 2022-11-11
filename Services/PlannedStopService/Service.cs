using System.Diagnostics;
using DTOs;
using PlannedStop.Queries;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlannedStopService
{
    public interface IPlannedStopService
    {
        Task<bool> SavePmTask(PmTaskDto pmTask);
        Task<bool> SaveCilTask(CilTaskDto cilTask);
        Task<bool> SaveClTask(ClTaskDto clTask);
        Task<bool> SaveOtherTask(OtherTaskDto otherTask);
        Task<PmTaskDto?> GetPmTask(Guid taskId);
        Task<CilTaskDto?> GetCilTask(Guid taskId);
        Task<ClTaskDto?> GetClTask(Guid taskId);
        Task<OtherTaskDto?> GetOtherTask(Guid taskId);
        Task<IEnumerable<PmTaskDto>> GetPmTasksForLine(int lineId, bool openOnly);
        Task<IEnumerable<CilTaskDto>> GetCilTasksForLine(int lineId, bool openOnly);
        Task<IEnumerable<ClTaskDto>> GetClTasksForLine(int lineId, bool openOnly);
        Task<IEnumerable<OtherTaskDto>> GetOtherTasksForLine(int lineId, bool openOnly);
        Task DeletePmTask(Guid taskId);
        Task DeleteCilTask(Guid taskId);
        Task DeleteClTask(Guid taskId);
        Task DeleteOtherTask(Guid taskId);
    }

    public class Service : IPlannedStopService
    {
        private HttpClient _client;

        public Service(IHttpClientFactory factory)
        {
            _client = factory.CreateClient(ClientNames.Names.PlannedStop);
        }

        public async Task<bool> SavePmTask(PmTaskDto pmTask)
        {
            var serialized = JsonSerializer.Serialize(pmTask);
            var result= await _client.PostAsJsonAsync( Queries.SavePmTask, pmTask);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> SaveCilTask(CilTaskDto cilTask)
        {
            var serialized = JsonSerializer.Serialize(cilTask);
            var result = await _client.PostAsJsonAsync(Queries.SaveCilTask, cilTask);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> SaveClTask(ClTaskDto clTask)
        {
            var serialized = JsonSerializer.Serialize(clTask);
            var result = await _client.PostAsJsonAsync(Queries.SaveClTask, clTask);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> SaveOtherTask(OtherTaskDto otherTask)
        {
            var serialized = JsonSerializer.Serialize(otherTask);
            var result = await _client.PostAsJsonAsync(Queries.SaveOtherTask, otherTask);
            return result.IsSuccessStatusCode;
        }

       

        public async Task<PmTaskDto?> GetPmTask(Guid taskId) => await _client.GetFromJsonAsync<PmTaskDto?>(Queries.GetPmTask(taskId));
        public async Task<CilTaskDto?> GetCilTask(Guid taskId) => await _client.GetFromJsonAsync<CilTaskDto?>(Queries.GetCilTask(taskId));
        public async Task<ClTaskDto?> GetClTask(Guid taskId) => await _client.GetFromJsonAsync<ClTaskDto?>(Queries.GetClTask(taskId));
        public async Task<OtherTaskDto?> GetOtherTask(Guid taskId) => await _client.GetFromJsonAsync<OtherTaskDto?>(Queries.GetOtherTask(taskId));

        public async Task<IEnumerable<PmTaskDto>> GetPmTasksForLine(int lineId, bool openOnly) => await _client.GetFromJsonAsync<IEnumerable<PmTaskDto>>(Queries.GetPmTasksForLine(lineId, openOnly));
        public async Task<IEnumerable<CilTaskDto>> GetCilTasksForLine(int lineId, bool openOnly) => await _client.GetFromJsonAsync<IEnumerable<CilTaskDto>>(Queries.GetCilTasksForLine(lineId, openOnly));

        public async Task<IEnumerable<ClTaskDto>> GetClTasksForLine(int lineId, bool openOnly) => await _client.GetFromJsonAsync<IEnumerable<ClTaskDto>>(Queries.GetClTasksForLine(lineId, openOnly));

        public async Task<IEnumerable<OtherTaskDto>> GetOtherTasksForLine(int lineId, bool openOnly) => await _client.GetFromJsonAsync<IEnumerable<OtherTaskDto>>(Queries.GetOtherTasksForLine(lineId, openOnly));

        public async Task DeletePmTask(Guid taskId)
        {
            try
            {
                await _client.PostAsJsonAsync(Queries.DeletePmTask, taskId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task DeleteCilTask(Guid taskId)
        {
            await _client.PostAsJsonAsync(Queries.DeleteCilTask, taskId);
        }
        public async Task DeleteClTask(Guid taskId)
        {
            await _client.PostAsJsonAsync(Queries.DeleteClTask, taskId);
        }
        public async Task DeleteOtherTask(Guid taskId)
        {
            await _client.PostAsJsonAsync(Queries.DeleteOtherTask, taskId);
        }
    }
}