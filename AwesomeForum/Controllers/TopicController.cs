﻿using AwesomeForum.Data.Services;
using AwesomeForum.Data.ViewModels;
using AwesomeForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace AwesomeForum.Controllers
{
    public class TopicController : Controller
    {
        private readonly string _apiUrl;
        private UserService _userService = new UserService();

        public TopicController(IConfiguration configuration)
        {
            _apiUrl = configuration.GetValue<string>("ApiUrl");
        }
        public IActionResult Details(int id = 7)
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            List<Forum> forums = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_apiUrl + ""))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (apiResponse != null)
                    {
                        forums = JsonConvert.DeserializeObject<List<Forum>>(apiResponse);
                    }
                }
            }

            if (forums == null)
            {
                forums = new List<Forum>
                {
                    new Forum {
                        id = 1,
                        name = "Rules & announcements",
                        topicCount = 3,
                        orderNr = 0,
                        categoryId = 1
                    },
                    new Forum {
                        id = 2,
                        name = "Welcome",
                        topicCount = 17,
                        orderNr = 1,
                        categoryId = 1
                    }
                };
            }

            ViewBag.Forums = new SelectList(forums, "Id", "Name", id.ToString());

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewTopicVM newTopic)
        {
            _userService.SetHttpContextUser(Request, HttpContext);

            Topic topic = null;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(newTopic), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(_apiUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse != null)
                    {
                        topic = JsonConvert.DeserializeObject<Topic>(apiResponse);
                    }
                }
            }
            return RedirectToAction("Details", "Topic", topic.id);
        }
    }
}
