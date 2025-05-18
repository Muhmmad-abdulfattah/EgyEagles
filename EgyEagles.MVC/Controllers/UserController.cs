using EgyEagles.MVC.Models;
using EgyEagles.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EgyEagles.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        // GET: /User
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            var role = HttpContext.Session.GetString("Role");
            var companyId = HttpContext.Session.GetString("CompanyId");

            if (role != "CompanyAdmin" || string.IsNullOrEmpty(companyId))
                return Forbid();

            var users = await _userAppService.GetUsersByCompanyAsync(companyId, token);
            return View(users);
        }

        // GET: /User/Create
        [HttpGet]
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

        // POST: /User/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = HttpContext.Session.GetString("JWT");
            var userId = await _userAppService.CreateUserAsync(model, token);

            if (userId == null)
            {
                Console.WriteLine("✅ Created UserId: " + userId);
                ModelState.AddModelError("", "Something went wrong while creating the user.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

    }
}
