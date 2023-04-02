using EDSU_SYSTEM.Data;
using EDSU_SYSTEM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EDSU_SYSTEM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Slider = _context.Sliders.Where(i => i.isActive == true).ToList();
            ViewBag.News = _context.LatestNews.Where(i => i.isActive ==  true).OrderByDescending(r => r.Id).Take(3).ToList();
            ViewBag.Activities = _context.Activities.Where(i => i.isActive ==  true).OrderByDescending(r => r.Id).Take(2).ToList();
            return View();
        }

        public IActionResult About()
        {
            return View();
        } 
        public IActionResult Programs()
        {
            var programs = (from s in _context.Programs where s.Active == true select s).Include(i => i.Departments).ThenInclude(i => i.Faculties).ToList();
            
            return View(programs);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult News()
        {
            var news = _context.LatestNews.ToList();
            return View(news);
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
        public IActionResult Partners()
        {
            return View();
        }
        public IActionResult Fees()
        {
            return View();
        }
        public IActionResult Directories()
        {
            return View();
        }
        
        public IActionResult Faqs()
        {
            return View();
        }
        public IActionResult Activities()
        {
            ViewBag.Activities2 = _context.Activities.Where(i => i.isActive == true).OrderByDescending(r => r.Id).Take(6).ToList();
            return View();
        }
        public IActionResult Events()
        {
            return View();
        }
         public IActionResult Magazines()
        {
            ViewBag.magazine = _context.Magazines.ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}