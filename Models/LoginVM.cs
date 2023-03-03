using EDSU_SYSTEM.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace EDSU_SYSTEM.Models
{
    public class LoginVM : PageModel
    {
        [BindProperty]
        public string? Email { get; set; }
        [BindProperty]
        public string? Password { get; set; }

        public void OnGet()
        {

        }
       
        public IActionResult OnPost()
        {
            bool isAuthenticated = AuthenticateUser(Email, Password);
            if (isAuthenticated)
            {
                HttpContext.Session.SetString("LoggedIn", "true");
                HttpContext.Session.SetString("Email", Email);
                Console.Write("Loggedin");
                return RedirectToPage("index", "home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Credentials");
                return Page();
            }
        }
        private readonly ApplicationDbContext _context;
        public LoginVM(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool AuthenticateUser(string Email, string Password)
        {

            if (Email != null || Password != null)
            {
                var user = _context.UgApplicants.FirstOrDefault(u => u.Email == Email);
                //var user = (from s in _context.UgApplicants where s.Email == Email && s.Password == Password select s).FirstOrDefault();
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
