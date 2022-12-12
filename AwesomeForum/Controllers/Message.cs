using AwesomeForum.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeForum.Controllers
{
    public class Message : Controller
    {
        [HttpGet]
        public IActionResult Create(int id = 8)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewTopicVM newTopic)
        {
            return RedirectToAction("Details", "Topic");
        }
    }
}
