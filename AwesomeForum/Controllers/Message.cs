using AwesomeForum.Data.ViewModels;
using AwesomeForum.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AwesomeForum.Controllers
{
    public class Message : Controller
    {
        private readonly string _apiUrl;

        public Message(IConfiguration configuration)
        {
            _apiUrl = configuration.GetValue<string>("ApiUrl");
        }

        [HttpGet]
        public IActionResult Create(int id = 8)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewTopicVM newTopic)
        {
            Topic topic = new Topic();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(newTopic), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(_apiUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    topic = JsonConvert.DeserializeObject<Topic>(apiResponse);
                }
            }
            return RedirectToAction("Details", "Topic", topic.Id);
        }
    }
}
