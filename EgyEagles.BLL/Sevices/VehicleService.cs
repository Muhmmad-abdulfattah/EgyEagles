using EgyEagles.BLL.Interfaces;
using EgyEagles.Domain.Enitites;
using EgyEagles.Domain.Interfaces;
using EgyEagles.Shared.DTOs.Companies;
using EgyEagles.Shared.DTOs.Vehicle;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.BLL.Sevices
{
    public class VehicleService : IVehicleService
    {

        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICompanyRepository _companyRepository;

        public VehicleService(IVehicleRepository vehicleRepository, ICompanyRepository companyRepository)
        {
            _vehicleRepository = vehicleRepository;
            _companyRepository = companyRepository;
        }
        public async Task<string> CreateVehicleAsync(CreateVehicleDto dto)
        {
            var company = await _companyRepository.GetByIdAsync(dto.CompanyId);
            if (company == null)
                throw new Exception("Company not found");

            var vehicle = new Vehicle
            {
                Name = dto.Name,
                CompanyId = dto.CompanyId,
                Latitude = 0,
                Longitude = 0
            };

            await _vehicleRepository.AddAsync(vehicle);

            company.VehicleIds.Add(vehicle.Id);
            await _companyRepository.UpdateAsync(company);

            return vehicle.Id;
        }

        public async Task<List<VehicleDto>> GetAllAsync()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            return vehicles.Select(v => new VehicleDto
            {
                Id = v.Id,
                Name = v.Name,
                CompanyId = v.CompanyId,
                Latitude = v.Latitude,
                Longitude = v.Longitude
            }).ToList();
        }

        public async Task<List<VehicleDto>> GetVehiclesByCompanyIdAsync(string companyId)
        {
            var vehicles = await _vehicleRepository.GetByCompanyIdAsync(companyId);
            return vehicles.Select(v => new VehicleDto
            {
                Id = v.Id,
                Name = v.Name,
                CompanyId = v.CompanyId,
                Latitude = v.Latitude,
                Longitude = v.Longitude
            }).ToList();
        }
        public async Task<bool> UpdateVehicleAsync(string id, UpdateVehicleDto dto)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return false;

            vehicle.Latitude = dto.Latitude;
            vehicle.Longitude = dto.Longitude;

            await _vehicleRepository.UpdateAsync(vehicle);
            return true;
        }

        public async Task UpdateLocationAsync(UpdateVehicleLocationDto dto)
        {
            if (!ObjectId.TryParse(dto.VehicleId, out ObjectId vehicleObjectId))
                throw new ArgumentException("Invalid VehicleId format");
            var vehicle = await _vehicleRepository.GetByIdAsync(dto.VehicleId);
            if (vehicle == null)
                throw new Exception("Vehicle not found");

            vehicle.Latitude = dto.Latitude;
            vehicle.Longitude = dto.Longitude;

            await _vehicleRepository.UpdateAsync(vehicle);
        }
    }
}
