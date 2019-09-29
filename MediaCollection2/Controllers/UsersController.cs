using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediaCollection2.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using MediaCollection2.Data;

namespace MediaCollection2.Controllers
{
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly MediaCollectionContext context;

        public UsersController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,MediaCollectionContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }
        public IActionResult Index()
        {
            var users = userManager.Users;
            List<UserViewModel> model = new List<UserViewModel>();
            foreach (var user in users)
            {
                model.Add(new UserViewModel() { id = user.Id,  }); 
            }
            return View(model);
        }
        public async Task<ActionResult> Edit(string id)
        {
            ViewBag.userId = id;
            var user = await userManager.FindByIdAsync(id);
            var model = new List<UserViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var UserViewModel = new UserViewModel
                {
                    id = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    UserViewModel.IsSelected = true;
                }
                else
                {
                    UserViewModel.IsSelected = false;
                }

                model.Add(UserViewModel);
            }
            return View(model);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(List<UserViewModel> model, string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (User == null)
            {
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var role = await roleManager.FindByIdAsync(model[i].id);

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
                        return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var model = new UserViewModel() { UserName = user.UserName, id = user.Id };
            return View(model);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Deleteconfirmed(string id)
        {
            if (id != null)
            {
                var user = await userManager.FindByIdAsync(id);
                var movies = context.Movies.Where(m => m.UserId == id);
                foreach (var movie in movies)
                {
                    context.Movies.Remove(movie);
                }
                context.SaveChanges();

                await userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}