using AwesomeForum.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Xml.Linq;

namespace AwesomeForum.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Details(int id = 7)
        {
            var forum = new Forum
            {
                Id = 1,
                Name = "Software",
                TopicCount = 2,
                Topics = new List<Topic>
                {
                    new Topic
                    {
                        Id = 1,
                        Name = "All about React",
                        Creator = new AppUser
                        {
                            UserName = "Copilul"
                        },
                        CreatorId = "7",
                        DateCreated = DateTime.Now.AddDays(-10),
                        LastPosted = DateTime.Now.AddDays(-3).AddMinutes(87),
                        MessageCount = 3
                    },
                    new Topic
                    {
                        Id = 2,
                        Name = "OpenGL Magic",
                        Creator = new AppUser
                        {
                            UserName = "Baiatul"
                        },
                        CreatorId = "7",
                        DateCreated = DateTime.Now.AddDays(-3),
                        LastPosted = DateTime.Now.AddDays(-1).AddMinutes(723),
                        MessageCount = 10
                    }
                }
            };

            return View(forum);
        }

        public IActionResult UserTopics(int id)
        {
            var forum = new Forum
            {
                Name = "Topicurile userului Copilul",
                TopicCount = 1,
                Topics = new List<Topic>
                {
                    new Topic
                    {
                        Name = "All about React",
                        Creator = new AppUser
                        {
                            UserName = "Copilul"
                        },
                        CreatorId = "7",
                        DateCreated = DateTime.Now.AddDays(-10),
                        LastPosted = DateTime.Now.AddDays(-3).AddMinutes(87),
                        MessageCount = 3

                    }
                }
            };

            return View("Details", forum);
        }
    }
}
