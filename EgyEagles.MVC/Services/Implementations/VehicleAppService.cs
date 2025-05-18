using EgyEagles.MVC.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EgyEagles.MVC.Services.Implementations
{
    public class VehicleAppService : IVehicleAppService
    {
        private readonly HttpClient _httpClient;

        public VehicleAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VehicleViewModel>> GetVehiclesByCompanyAsync(string companyId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"api/vehicle/by-company/{companyId}");

            if (!response.IsSuccessStatusCode)
                return new List<VehicleViewModel>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<VehicleViewModel>>(json);
        }

        public async Task<string> CreateVehicleAsync(CreateVehicleViewModel model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/vehicle/create", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(result);
            return obj?.vehicleId;
        }

        public async Task<bool> UpdateVehicleLocationAsync(string vehicleId, double latitude, double longitude, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var payload = new
            {
                VehicleId = vehicleId,
                Latitude = latitude,
                Longitude = longitude
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/vehicle/update-location", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<VehicleViewModel>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("api/vehicle");
            if (!response.IsSuccessStatusCode)
                return new List<VehicleViewModel>();

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<VehicleViewModel>>(result);


        }
        public async Task<bool> UpdateVehicleAsync(UpdateVehicleViewModel model, string token)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsync($"api/vehicle/{model.Id}", content);

            return response.IsSuccessStatusCode;
        }

    }
}