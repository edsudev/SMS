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
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public FileResult DownloadMagazine(string name)
        {
            string path = Path.Combine(this._hostingEnvironment.WebRootPath, "files/magazines/file/") + name;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", name);
        }
        public IActionResult Index()
        {
            ViewBag.Slider = _context.Sliders?.Where(i => i.isActive == true).ToList();
            ViewBag.News = _context.LatestNews?.Where(i => i.isActive ==  true).OrderByDescending(r => r.Id).Take(3).ToList();
            ViewBag.Activities = _context.Activities?.Where(i => i.isActive ==  true).OrderByDescending(r => r.Id).Take(2).ToList();
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
            var news = _context.LatestNews?.ToList();
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
            var staffs = _context.Staffs?.Include(x => x.Positions).Include(x => x.Departments).ToList();
            return View(staffs);
        }
        
        public IActionResult Faqs()
        {
            return View();
        }
        public IActionResult Activities()
        {
            ViewBag.Activities2 = _context.Activities?.Where(i => i.isActive == true).OrderByDescending(r => r.Id).Take(6).ToList();
            return View();
        }
        public IActionResult Events()
        {
            var events = (from s in _context.Events select s).ToList();
            return View(events);
        }
         public IActionResult Magazines()
        {
            ViewBag.magazine = _context.Magazines?.ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}