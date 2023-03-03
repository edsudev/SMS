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
            return View();
        }

        public IActionResult About()
        {
            return View();
        } 
        public IActionResult Programs()
        {
            var programs = (from s in _context.Programs select s).ToList();
            
            return View(programs);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
         public IActionResult Event()
        {
            return View();
        }
         public IActionResult Magazine()
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