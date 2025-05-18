using EgyEagles.MVC.Models;
using EgyEagles.MVC.Services;
using EgyEagles.MVC.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace EgyEagles.MVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyAppService _companyAppService;
        private readonly IUserAppService _userAppService;

        public CompanyController(ICompanyAppService companyAppService,IUserAppService userAppService)
        {
            _companyAppService = companyAppService;
            _userAppService = userAppService;
        }

 
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            var role = HttpContext.Session.GetString("Role"); 
            ViewBag.Role = role;

            var companies = await _companyAppService.GetAllCompanyNamesAsync(token);
            return View(companies);
        }

        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("Role");
            var companyId = HttpContext.Session.GetString("CompanyId");

            if (role != "CompanyAdmin" || string.IsNullOrEmpty(companyId))
                return Forbid();

            var model = new CreateUserViewModel
            {
                CompanyId = companyId 
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "CompanyAdmin")
                return Forbid();

            var token = HttpContext.Session.GetString("JWT");
            await _userAppService.CreateUserAsync(model, token);

            return RedirectToAction("Index");
        }


    }
}