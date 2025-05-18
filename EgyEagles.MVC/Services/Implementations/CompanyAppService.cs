using EgyEagles.MVC.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EgyEagles.MVC.Services.Implementations
{
    public class CompanyAppService : ICompanyAppService
    {
        private readonly HttpClient _httpClient;

        public CompanyAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CompanyViewModel>> GetAllCompanyNamesAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("api/company/names");
            if (!response.IsSuccessStatusCode)
                return new List<CompanyViewModel>();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CompanyViewModel>>(result);
        }

        public async Task<string> CreateCompanyAsync(CreateCompanyViewModel model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/company/create", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(result);
            return obj?.companyId;
        }

        public async Task<CompanyViewModel> GetCompanyByIdAsync(string id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"api/company/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CompanyViewModel>(result);
        }
    }
}