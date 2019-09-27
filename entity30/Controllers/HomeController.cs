using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using entity30.Models;
using entity30.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace entity30.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BloggingContext _context;

        public HomeController(ILogger<HomeController> logger, BloggingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public Blog blog = new Blog
        {
            Url = "http://xcx.pl",
            Rating = 2,
            Posts = new List<Post>
            {
                new Post
                {
                    Title = "Test",
                    Content = "DUpadupadsada",
                    Rating = 3
                },
                new Post
                {
                    Title = "Drugi",
                    Content = "lalalala",
                    Rating = 2
                }
            }
        };

        public IActionResult Privacy()
        {
            var blogs = _context.Blogs.Include(blog => blog.Posts).ToList();
            /*
             * To create entry use this
            _context.Blogs.Add(blog);       
            _context.SaveChanges();
            */
            return Content(JsonSerializer.Serialize(blog));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
