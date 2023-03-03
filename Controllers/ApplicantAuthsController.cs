using EDSU_SYSTEM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDSU_SYSTEM.Controllers
{
    public class ApplicantAuthsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ApplicantAuthsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            
            return View();
        }
    }
}
