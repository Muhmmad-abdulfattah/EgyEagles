using EgyEagles.MVC.Models;
using EgyEagles.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EgyEagles.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAppService _userAppService;

        public AccountController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var tokenResponse = await _userAppService.LoginAsync(model);

            if (tokenResponse == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            HttpContext.Session.SetString("JWT", tokenResponse.Token);
            HttpContext.Session.SetString("Role", tokenResponse.Role ?? "");
            HttpContext.Session.SetString("UserId", tokenResponse.UserId ?? "");
            HttpContext.Session.SetString("CompanyId", tokenResponse.CompanyId ?? "");

            return RedirectToAction("Index", "Home");
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
