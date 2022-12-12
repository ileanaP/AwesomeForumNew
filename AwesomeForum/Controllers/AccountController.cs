using AwesomeForum.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AwesomeForum.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM newLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(newLogin);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Profile(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM newRegister)
        {
            if (!ModelState.IsValid)
            {
                return View(newRegister);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(RegisterVM registerVM)
        {
            return RedirectToAction("Index", "Account");
        }
    }
}
