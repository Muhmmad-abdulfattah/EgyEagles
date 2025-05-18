using EgyEagles.MVC.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EgyEagles.MVC.Services.Implementations
{
    public class UserAppService : IUserAppService
    {
        private readonly HttpClient _httpClient;

        public UserAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> CreateUserAsync(CreateUserViewModel model, string token)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("api/user/create", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("❌ API ERROR: " + error);
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();

            dynamic resObj = JsonConvert.DeserializeObject(result);
            return resObj?.userId;
        }


        public async Task<List<UserViewModel>> GetUsersByCompanyAsync(string companyId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"api/user/by-company/{companyId}");
            if (!response.IsSuccessStatusCode)
                return new List<UserViewModel>();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserViewModel>>(result);
        }

        public async Task<TokenResponseModel> LoginAsync(LoginViewModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/user/login", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API Response: " + result); // ← اطبعها أو اعمل Log

            return JsonConvert.DeserializeObject<TokenResponseModel>(result);
        }
    }
}
