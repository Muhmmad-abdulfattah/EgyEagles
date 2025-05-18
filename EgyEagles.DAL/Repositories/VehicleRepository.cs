using EgyEagles.DAL.Context;
using EgyEagles.Domain.Enitites;
using EgyEagles.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyEagles.DAL.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _vehicles;
        public VehicleRepository(MongoDbContext context) : base(context)
        {
            _vehicles = context.Vehicles;
        }

        public async Task<List<Vehicle>> GetByCompanyIdAsync(string companyId)
        {
            if (!ObjectId.TryParse(companyId, out ObjectId objectId))
                throw new ArgumentException("Invalid ObjectId format");
            return await _vehicles.Find(v => v.CompanyId == companyId).ToListAsync();
        }
    }
}
