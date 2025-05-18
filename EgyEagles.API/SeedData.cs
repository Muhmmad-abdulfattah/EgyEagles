using EgyEagles.DAL.Context;
using EgyEagles.DAL.Repositories;
using EgyEagles.Domain.Enitites;
using EgyEagles.Domain.Enums;

namespace EgyEagles.API
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<MongoDbContext>();
            var companyRepo = new CompanyRepository(context);
            var userRepo = new UserRepository(context);
            var vehicleRepo = new VehicleRepository(context);

            var existingSuper = await userRepo.GetByEmailAsync("super@admin.com");
            if (existingSuper != null)
                return;

            // 1. Company
            var company = new Company { Name = "Demo Company", UserIds = new(), VehicleIds = new() };
            await companyRepo.AddAsync(company);

            // 2. SuperAdmin
            var superAdmin = new User
            {
                Email = "super@admin.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = UserRole.SuperAdmin,
                Permissions = new List<string>(),
                CompanyId = ""
            };
            await userRepo.AddAsync(superAdmin);

            // 3. CompanyAdmin
            var companyAdmin = new User
            {
                Email = "admin@demo.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = UserRole.CompanyAdmin,
                CompanyId = company.Id,
                Permissions = new List<string> { "ManageUsers", "ManageVehicles" }
            };
            await userRepo.AddAsync(companyAdmin);

            // 4. Regular User
            var user = new User
            {
                Email = "user@demo.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = UserRole.RegularUser,
                CompanyId = company.Id,
                Permissions = new List<string>()
            };
            await userRepo.AddAsync(user);

            // 5. Vehicles
            await vehicleRepo.AddAsync(new Vehicle { Name = "Toyota", CompanyId = company.Id, Latitude = 0, Longitude = 0 });
            await vehicleRepo.AddAsync(new Vehicle { Name = "Nissan", CompanyId = company.Id, Latitude = 0, Longitude = 0 });
        }

    }

}
