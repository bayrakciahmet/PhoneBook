using PhoneBook.Shared.Dtos;
using PhoneBook.Web.Models.ContactInfos;
using System.Net.Http.Json;

namespace PhoneBook.Web.Services.ContactInfo
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

        public async Task<bool> CreateContactInfoAsync(ContactInfoCreateInput contactInfoCreateInput)
        {
            var response = await _client.PostAsJsonAsync<ContactInfoCreateInput>("contactinfos", contactInfoCreateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateContactInfoAsync(ContactInfoUpdateInput contactInfoUpdateInput)
        {
            var response = await _client.PutAsJsonAsync<ContactInfoUpdateInput>("contactinfos", contactInfoUpdateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteContactInfoAsync(string id)
        {
            var response = await _client.DeleteAsync($"contactinfos?id={id}");
            return response.IsSuccessStatusCode;
        }
    }
}
