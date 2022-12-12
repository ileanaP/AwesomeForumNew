using AwesomeForum.Data.ViewModels;
using AwesomeForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AwesomeForum.Controllers
{
    public class TopicController : Controller
    {
        public IActionResult Details(int id = 7)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var forums = new List<Forum>();
            forums.Add(new Forum {
                Id = 1,
                Name = "Forum 1"
            });
            forums.Add(new Forum
            {
                Id = 2,
                Name = "Forum 2"
            });

            ViewBag.Forums = new SelectList(forums, "Id", "Name", id.ToString());

            return View();
        }

        [HttpPost]
        public IActionResult Create(NewTopicVM newTopic)
        {
            return RedirectToAction("Details", "Forum");
        }
    }
}
