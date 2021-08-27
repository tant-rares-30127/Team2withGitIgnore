using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team2Application.Data;
using Team2Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Team2Application.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<IdentityUser> userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await userManager.Users.ToListAsync());
        }

        public async Task<IActionResult> AssignAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.AddToRoleAsync(user, "Administrator");
/*            userManager.FindByIdAsync(id).Result.Role = "Administrator";*/
            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AssignUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Administrator");
/*            userManager.FindByIdAsync(id).Result.Role = "Basic User";*/
            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
