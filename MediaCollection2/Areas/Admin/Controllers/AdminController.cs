using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediaCollection2.Models.Admin;

namespace MediaCollection2.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        // GET: Admin
        public ActionResult Index()
        {
           var roles = roleManager.Roles;
            return View(roles);
        }

        // GET: Admin/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var role = await roleManager.FindByIdAsync(id);
            var model = new DetailRoleViewModel() { id = role.Id, RoleName = role.Name };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user,role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = model.RoleName,
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (IdentityError  error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
           var role = roleManager.Roles.FirstOrDefault(r => r.Id == id);
            var model = new EditRoleViewModel() { id = role.Id, RoleName = role.Name };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = roleManager.Roles.FirstOrDefault(r => r.Id == id);
                role.Name = model.RoleName;
                await roleManager.UpdateAsync(role);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var model = new DeleteRoleViewModel() { RoleName = role.Name, id = role.Id };
            return View(model);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (id!=null)
            {
                var role = await roleManager.FindByIdAsync(id);
                await roleManager.DeleteAsync(role);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    userId = user.Id,
                    UserName = user.UserName
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

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].userId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("Edit", new { Id = roleId });
                }
            }

            return RedirectToAction("Edit", new { Id = roleId });
        }
        //[HttpGet]
        //public async Task<ActionResult> EditUsersInRole(string roleId)
        //{
        //    ViewBag.roleId = roleId;
        //    var role = await roleManager.FindByIdAsync(roleId);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    var model = new List<UserRoleViewModel>();
        //    foreach (var user in userManager.Users)
        //    {
        //        var userRoleViewModel = new UserRoleViewModel
        //        {
        //            userId = user.Id,
        //            UserName = user.UserName,
        //        };
        //        if (await userManager.IsInRoleAsync(user,role.Name))
        //        {
        //            userRoleViewModel.IsSelected = true;
        //        }
        //        else
        //        {
        //            userRoleViewModel.IsSelected = false;
        //        }
        //        model.Add(userRoleViewModel);
        //    }
        //    return View(model);
        //}
        //[HttpPost]
        //public async Task<IActionResult> EditUsersInRole(string roleId, List<UserRoleViewModel> model)
        //{
        //    var role = await roleManager.FindByIdAsync(roleId);
        //    foreach (var user in model)
        //    {
        //        var User = await userManager.FindByIdAsync(user.userId);
        //        IdentityResult result = null;
        //        if (user.IsSelected&& !(await userManager.IsInRoleAsync(User,role.Name)))
        //        {
        //            result = await userManager.AddToRoleAsync(User, role.Name);
        //        }
        //        else if (!user.IsSelected && (await userManager.IsInRoleAsync(User, role.Name)))
        //        {
        //            result = await userManager.RemoveFromRoleAsync (User, role.Name);
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //    return RedirectToAction("Edit", new { id = roleId });
        //}
    }
}