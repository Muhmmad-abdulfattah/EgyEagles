using EgyEagles.Shared.DTOs.Companies;
using EgyEagles.Shared.DTOs.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.BLL.Interfaces
{
    public interface IVehicleService
    {
        Task<string> CreateVehicleAsync(CreateVehicleDto dto);
        Task UpdateLocationAsync(UpdateVehicleLocationDto dto);
        Task<List<VehicleDto>> GetVehiclesByCompanyIdAsync(string companyId);
        Task<List<VehicleDto>> GetAllAsync();
        Task<bool> UpdateVehicleAsync(string id, UpdateVehicleDto dto);
    }
}
