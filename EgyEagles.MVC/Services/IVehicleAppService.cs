using EgyEagles.MVC.Models;

public interface IVehicleAppService
{
    Task<List<VehicleViewModel>> GetVehiclesByCompanyAsync(string companyId, string token);
    Task<string> CreateVehicleAsync(CreateVehicleViewModel model, string token);
    Task<bool> UpdateVehicleLocationAsync(string vehicleId, double latitude, double longitude, string token);
    Task<List<VehicleViewModel>> GetAllAsync(string token);

    Task<bool> UpdateVehicleAsync(UpdateVehicleViewModel model, string token);


}