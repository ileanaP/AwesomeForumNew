using AwesomeForum.Data.Services;
using AwesomeForum.Data.ViewModels;
using AwesomeForum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace AwesomeForum.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _apiUrl;
        private UserService _userService = new UserService();

        public AccountController(IConfiguration configuration)
        {
            _apiUrl = configuration.GetValue<string>("ApiUrl");
        }

        public async Task<IActionResult> Index()
        {
            if (!_userService.UserLoggedIn(Request))
            {
                return RedirectToAction("Index", "Home");
            }

            _userService.SetHttpContextUser(Request, HttpContext);


            AppUser user = _userService.GetLoggedInUser(Request);
            //return View(user);
            return View(new AppUser());
        }

        [HttpGet]
        public IActionResult Login()
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM newLogin)
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            if (!ModelState.IsValid)
            {
                return View(newLogin);
            }

            using (var httpClient = new HttpClient())
            {
                var tmp1 = new { username = newLogin.EmailOrUsername, password = newLogin.Password };
                StringContent content = new StringContent(JsonConvert.SerializeObject(tmp1), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(_apiUrl + "/login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    apiResponse = "{\"id\":2,\"username\":\"ileana\",\"password\":\"qwerty\",\"email\":\"ileana@gmail.com\",\"nrOfMessages\":0,\"nrOfTopics\":0}";
                    var user = JsonConvert.DeserializeObject<AppUser>(apiResponse);

                    _userService.SetUserLoginCookie(Response, user);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {
            _userService.SetHttpContextUser(Request, HttpContext);

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
            _userService.SetHttpContextUser(Request, HttpContext);

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
                    apiResponse = "{\"id\":2,\"username\":\"ileana\",\"password\":\"qwerty\",\"email\":\"ileana@gmail.com\",\"nrOfMessages\":0,\"nrOfTopics\":0}";
                    var user = JsonConvert.DeserializeObject<AppUser>(apiResponse);

                    _userService.SetUserLoginCookie(Response, user);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("UserNrOfMessages");
            Response.Cookies.Delete("UserNrOfTopics");

            HttpContext.User = null;

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(RegisterVM registerVM)
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            //AppUser user = await _userManager.GetUserAsync(principal: HttpContext.User);
            AppUser user = new AppUser();
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

                    
                }
            }
            return RedirectToAction("Index", "Account");
        }
    }
}
