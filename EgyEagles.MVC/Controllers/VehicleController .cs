using EgyEagles.MVC.Models;
using EgyEagles.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EgyEagles.MVC.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleAppService _vehicleAppService;

        public VehicleController(IVehicleAppService vehicleAppService)
        {
            _vehicleAppService = vehicleAppService;
        }

        public async Task<IActionResult> Index(string? companyId)
        {
            var token = HttpContext.Session.GetString("JWT");
            var role = HttpContext.Session.GetString("Role");
            var currentCompanyId = HttpContext.Session.GetString("CompanyId");

            List<VehicleViewModel> vehicles;

            if (role == "SuperAdmin")
            {
                if (string.IsNullOrEmpty(companyId))
                    vehicles = await _vehicleAppService.GetAllAsync(token);
                else
                    vehicles = await _vehicleAppService.GetVehiclesByCompanyAsync(companyId, token);
            }
            else if (role == "CompanyAdmin")
            {
                vehicles = await _vehicleAppService.GetVehiclesByCompanyAsync(currentCompanyId, token);
            }
            else
            {
                return Forbid();
            }

            return View(vehicles);
        }

        // GET: /Vehicle/Create
        [HttpGet]
        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("Role");
            var companyId = HttpContext.Session.GetString("CompanyId");

            if (role != "CompanyAdmin")
                return Forbid();

            var model = new CreateVehicleViewModel { CompanyId = companyId };
            return View(model);
        }

        // POST: /Vehicle/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleViewModel model)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "CompanyAdmin")
                return Forbid();

            if (!ModelState.IsValid)
                return View(model);

            var token = HttpContext.Session.GetString("JWT");
            await _vehicleAppService.CreateVehicleAsync(model, token);

            return RedirectToAction("Index", new { companyId = model.CompanyId });
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return View(new UpdateVehicleViewModel { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateVehicleViewModel model)
        {
            var token = HttpContext.Session.GetString("JWT");
            var result = await _vehicleAppService.UpdateVehicleAsync(model, token);

            if (!result)
            {
                ModelState.AddModelError("", "Update failed.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

    }
}
