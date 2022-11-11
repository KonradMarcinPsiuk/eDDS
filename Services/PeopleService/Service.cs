using System.Net.Http.Json;

using DTOs;

using People.Queries;

namespace PeopleService
{
    public interface IPeopleService
    {
        Task<IEnumerable<PersonDto>> GetPeopleForDepartment(int deptId);
        Task<IEnumerable<PersonDto>> GetPeopleForPlant(int plantId);
        Task<bool> SavePerson(PersonDto person);
        Task<bool> DeletePerson(int id);
    }

    public class Service : IPeopleService
    {
        private readonly HttpClient _httpClient;

        public Service(HttpClient client)
        {
            _httpClient = client;
        }

        public Service(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient(ClientNames.Names.PeopleClient);
        }

        public async Task<IEnumerable<PersonDto>> GetPeopleForDepartment(int deptId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<PersonDto>>(Queries.GetPeople(null, null, deptId)) ?? Array.Empty<PersonDto>();
        }

        public async Task<IEnumerable<PersonDto>> GetPeopleForPlant(int plantId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<PersonDto>>(Queries.GetPeople(plantId, null, null)) ?? Array.Empty<PersonDto>();
        }

        public async Task<bool> SavePerson(PersonDto person)
        {
            var result = await _httpClient.PostAsJsonAsync(Queries.SavePerson(), person);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var result =await _httpClient.DeleteAsync(Queries.DeletePerson(id));
            return result.IsSuccessStatusCode;
        }
    }
}
