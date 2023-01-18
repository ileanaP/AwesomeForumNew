using AwesomeForum.Data.Services;
using AwesomeForum.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Security.Principal;

namespace AwesomeForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _apiUrl;
        private UserService _userService = new UserService();

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _apiUrl = configuration.GetValue<string>("ApiUrl");
        }

        public async Task<IActionResult> Index()
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            List<Category> categories = new List<Category>();
            using (var httpClient = new HttpClient())
            {
                //using (var response = await httpClient.GetAsync(_apiUrl + "/categories"))
                using (var response = await httpClient.GetAsync("https://catfact.ninja/fact"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    apiResponse = "[{\"id\":1,\"name\":\"cat1\",\"orderNr\":1,\"forums\":[{\"id\":1,\"name\":\"for1\",\"orderNr\":1,\"categoryId\":1},{\"id\":2,\"name\":\"for2\",\"orderNr\":2,\"categoryId\":1}]},{\"id\":2,\"name\":\"cat2\",\"orderNr\":2,\"forums\":[{\"id\":3,\"name\":\"for3\",\"orderNr\":3,\"categoryId\":2},{\"id\":4,\"name\":\"for4\",\"orderNr\":4,\"categoryId\":2}]}]";
                    if (apiResponse != null)
                    {
                        categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                    }
                    else
                    {
                        categories = new List<Category>
                        {
                            new Category
                            {
                                Id = 1,
                                Name = "Administrative",
                                OrderNr = 0,
                                Forums = new List<Forum> {
                                new Forum {
                                    Id = 1,
                                    Name = "Rules & announcements",
                                    TopicCount = 3,
                                    OrderNr = 0,
                                    CategoryId = 1
                                },
                                new Forum {
                                    Id = 2,
                                    Name = "Welcome",
                                    TopicCount = 17,
                                    OrderNr = 1,
                                    CategoryId = 1
                                }
                            }
                            },
                            new Category
                            {
                                Id = 2,
                                Name = "IT Talks",
                                OrderNr = 1,
                                Forums = new List<Forum> {
                                new Forum {
                                    Id = 3,
                                    Name = "Software",
                                    TopicCount = 28,
                                    OrderNr = 0,
                                    CategoryId = 1
                                },
                                new Forum {
                                    Id = 4,
                                    Name = "Hardware",
                                    TopicCount = 5,
                                    OrderNr = 1,
                                    CategoryId = 1
                                },
                                new Forum {
                                    Id = 5,
                                    Name = "DevOps",
                                    TopicCount = 67,
                                    OrderNr = 1,
                                    CategoryId = 1
                                }
                            }
                            }

                        };
                    }
                }
            }

            return View(categories);
        }

        public IActionResult Privacy()
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}