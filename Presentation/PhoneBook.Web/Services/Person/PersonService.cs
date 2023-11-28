using PhoneBook.Shared.Dtos;
using PhoneBook.Web.Models.Persons;

namespace PhoneBook.Web.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _client;
        public PersonService(HttpClient client)
        {
            _client = client;
        }
       

        public async Task<List<PersonViewModel>> GetAllPersonAsync()
        {
            var response = await _client.GetAsync("persons");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<PersonViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<PersonViewModel> GetByPersonId(string personId)
        {
            var response = await _client.GetAsync($"persons/{personId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<PersonViewModel>>();

            return responseSuccess.Data;
        }

        public async Task<bool> CreatePersonAsync(PersonCreateInput personCreateInput)
        {
            var response = await _client.PostAsJsonAsync<PersonCreateInput>("persons", personCreateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdatePersonAsync(PersonUpdateInput personUpdateInput)
        {
            var response = await _client.PutAsJsonAsync<PersonUpdateInput>("persons", personUpdateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeletePersonAsync(string personId)
        {
            var response = await _client.DeleteAsync($"persons?id={personId}");
            return response.IsSuccessStatusCode;
        }
    }
}
