using AwesomeForum.Data.ViewModels;
using AwesomeForum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace AwesomeForum.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private readonly string _apiUrl;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.GetUserAsync(principal: HttpContext.User);
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM newLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(newLogin);
            }

            // AppUser user = new AppUser();
            using (var httpClient = new HttpClient())
            {
                var tmp1 = new { username = newLogin.EmailOrUsername, password = newLogin.Password };
                StringContent content = new StringContent(JsonConvert.SerializeObject(tmp1), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44324/api/Reservation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<AppUser>(apiResponse);

                    AppUser user = new AppUser
                    {
                        NrOfMessages = 100,
                        NrOfTopics = 50,
                        UserName = "Bob"
                    };

                    _signInManager.SignInAsync(user, true);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {
            AppUser user = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_apiUrl + ""))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (apiResponse != null)
                    {
                        user = JsonConvert.DeserializeObject<AppUser>(apiResponse);
                    }
                }
            }

            if (user == null)
            {
                user = new AppUser
                {
                    UserName = "Enrique",
                    NrOfMessages = 100,
                    NrOfTopics = 50
                };
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM newRegister)
        {
            if (!ModelState.IsValid)
            {
                return View(newRegister);
            }

            // AppUser user = new AppUser();
            using (var httpClient = new HttpClient())
            {
                var tmp1 = new
                {
                    email = newRegister.Email,
                    username = newRegister.Username,
                    password = newRegister.Password,
                    repeatPassword = newRegister.RepeatPassword
                };
                StringContent content = new StringContent(JsonConvert.SerializeObject(tmp1), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(_apiUrl + "", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<AppUser>(apiResponse);

                    AppUser user = new AppUser
                    {
                        NrOfMessages = 100,
                        NrOfTopics = 50,
                        UserName = "Bob"
                    };

                    _signInManager.SignInAsync(user, true);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(RegisterVM registerVM)
        {
            AppUser user = await _userManager.GetUserAsync(principal: HttpContext.User);
            using (var httpClient = new HttpClient())
            {
                var tmp1 = new
                {
                    id = user.Id,
                    oldPassword = registerVM.Password,
                    newPassword = registerVM.RepeatPassword
                };
                StringContent content = new StringContent(JsonConvert.SerializeObject(tmp1), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(_apiUrl + "", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var tmp = JsonConvert.DeserializeObject<bool>(apiResponse);

                    user = new AppUser
                    {
                        NrOfMessages = 100,
                        NrOfTopics = 50,
                        UserName = "Bob"
                    };

                    _signInManager.SignInAsync(user, true);
                }
            }
            return RedirectToAction("Index", "Account");
        }
    }
}
