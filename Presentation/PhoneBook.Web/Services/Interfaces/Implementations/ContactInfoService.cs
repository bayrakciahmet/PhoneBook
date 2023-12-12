using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.Shared.Dtos;
using PhoneBook.Web.Models.ContactInfos;

namespace PhoneBook.Web.Services.Interfaces.Implementations
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly HttpClient _client;
        public ContactInfoService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ContactInfoViewModel>> GetAllContactInfoAsync()
        {
            var response = await _client.GetAsync("contactinfos");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ContactInfoViewModel>>>();
            return responseSuccess.Data;
        }

        public async Task<List<ContactInfoViewModel>> GetAllContactInfoPersonIdAsync(string personId)
        {
            var response = await _client.GetAsync($"contactinfos/GetAllByPersonId/{personId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ContactInfoViewModel>>>();
            return responseSuccess.Data;
        }
        public async Task<ContactInfoViewModel> GetByContactInfoId(string id)
        {
            var response = await _client.GetAsync($"contactinfos/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<ContactInfoViewModel>>();
            return responseSuccess.Data;
        }

        public async Task<Response<ContactInfoViewModel>> CreateContactInfoAsync(ContactInfoCreateInput contactInfoCreateInput)
        {
            var response = await _client.PostAsJsonAsync("contactinfos", contactInfoCreateInput);
            var responseData = await response.Content.ReadFromJsonAsync<Response<ContactInfoViewModel>>();
            return responseData;
        }
        public async Task<Response<NoContent>> UpdateContactInfoAsync(ContactInfoUpdateInput contactInfoUpdateInput)
        {
            var response = await _client.PutAsJsonAsync("contactinfos", contactInfoUpdateInput);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return new Response<NoContent>() { IsSuccessful = true };
            else
            {
                var responseData = await response.Content.ReadFromJsonAsync<Response<NoContent>>();
                return responseData;
            }
        }
        public async Task<bool> DeleteContactInfoAsync(string id)
        {
            var response = await _client.DeleteAsync($"contactinfos?id={id}");
            return response.IsSuccessStatusCode;
        }
    }
}
