using AwesomeForum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.Xml;

namespace AwesomeForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Category> categories = new List<Category>
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

            // categories will be fetched from the back end
            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
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