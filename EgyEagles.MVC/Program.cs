using EgyEagles.MVC.Services;
using EgyEagles.MVC.Services.Implementations;

namespace EgyEagles.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddSession();

            builder.Services.AddHttpClient<IUserAppService, UserAppService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7129/"); 
            });
             builder.Services.AddHttpClient<ICompanyAppService, CompanyAppService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7129/"); 
            });
             builder.Services.AddHttpClient<IVehicleAppService, VehicleAppService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7129/"); 
            });


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}
