using EDSU_SYSTEM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EDSU_SYSTEM.Controllers
{
    [Authorize(Roles = "superAdmin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new()
                {
                    Name = model.Name
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
       // [Authorize(Roles = "superAdmin")]
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            roleManager.Dispose();
            return View(roles);
        }
       // [Authorize(Roles = "superAdmin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var services = new ServiceCollection();
            var serviceProvider = services.BuildServiceProvider();
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return View("Error");
            }
            var model = new EditRoleVM
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users?.Add(user.UserName);
                }
            }

            return View(model); 
        }
        //[Authorize(Roles = "superAdmin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleVM model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                return View("Error");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            return View(model); 
            }
        }
      //  [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> EditUserRole(string? id)
        {
            
            ViewBag.roleId = id;
            TempData["id"] = id;
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var users = await userManager.Users.Where(x=>x.Type == 2).ToListAsync();

            var model = new List<UserRoleVM>();

            foreach (var user in users)
            {
                var userRoleViewModel = new UserRoleVM
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }
            //TempData["UserRoleModel"] = model; // Store the model list in TempData
            return View(model);
        }

     //   [Authorize(Roles = "superAdmin")]
        [HttpPost]
        public async Task<IActionResult> EditUserRole(List<UserRoleVM> model, string? id)
        {
            id = (string)TempData["id"];
            Console.WriteLine("This is the id from post " );
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var storedModel = TempData["UserRoleModel"] as List<UserRoleVM>;
            foreach (var userRoleViewModel in storedModel)
            {
                var user = await userManager.FindByIdAsync(userRoleViewModel.UserId);

                if (user == null)
                {
                    continue;
                }

                if (userRoleViewModel.IsSelected)
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction("Index", "UserRoleManagement"); // Replace with appropriate redirect action and controller
        }

        //public async Task<IActionResult> EditUserRole(List<UserRoleVM> model)
        //{
        //    string roleId = (string)TempData["roleId"];
        //    var role = await roleManager.FindByIdAsync(roleId);

        //    if (role == null)
        //    {
        //        ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
        //        return View("NotFound");
        //    }

        //    for (int i = 0; i < model.Count; i++)
        //    {
        //        var user = await userManager.FindByIdAsync(model[i].UserId);

        //        IdentityResult result = null;

        //        if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
        //        {
        //            result = await userManager.AddToRoleAsync(user, role.Name);
        //        }
        //        else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            result = await userManager.RemoveFromRoleAsync(user, role.Name);
        //        }
        //        else
        //        {
        //            continue;
        //        }

        //        if (result.Succeeded)
        //        {
        //            if (i < (model.Count - 1))
        //                continue;
        //            else
        //                return RedirectToAction("EditRole", new { Id = roleId });
        //        }
        //    }

        //    return RedirectToAction("EditRole", new { Id = roleId });
        //}
    }
}
