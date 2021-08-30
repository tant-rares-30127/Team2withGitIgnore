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
using Microsoft.AspNetCore.Authorization;

namespace Team2Application.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private UserManager<IdentityUser> userManager;
        public static readonly string ADMIN_ROLE = "Administrator";
        public static readonly string OPERATOR_ROLE = "Operator";
        public static readonly string REGULAR_USER_ROLE = "User";
        //public static readonly string VISITOR_ROLE = "Visitor";

        public UsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }



        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await GetUsersWithRole());
        }

        public async Task<IActionResult> AssignAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.AddToRoleAsync(user, "Administrator");
            await userManager.RemoveFromRoleAsync(user, "Operator");
            await userManager.RemoveFromRoleAsync(user, "User");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AssignUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Administrator");
            await userManager.RemoveFromRoleAsync(user, "Operator");
            await userManager.AddToRoleAsync(user, "User");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AssignOperator(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Administrator");
            await userManager.AddToRoleAsync(user, "Operator");
            await userManager.RemoveFromRoleAsync(user, "User");
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<UserDto>> GetUsersWithRole()
        {
            List<UserDto> usersList = new List<UserDto>();
            var allUsers = await userManager.Users.ToListAsync();
            var admins = await userManager.GetUsersInRoleAsync(ADMIN_ROLE);
            var operators = await userManager.GetUsersInRoleAsync(OPERATOR_ROLE);
            //var users = await userManager.GetUsersInRoleAsync(REGULAR_USER_ROLE);

            var users = allUsers.Except(operators).ToList();
            users = users.Except(admins).ToList();

            foreach (var admin in admins)
            {
                usersList.Add(new UserDto(admin.Id, admin.Email, ADMIN_ROLE));
            }

            foreach (var operatorv in operators)
            {
                usersList.Add(new UserDto(operatorv.Id, operatorv.Email, OPERATOR_ROLE));
            }

            foreach (var user in users)
            {
                usersList.Add(new UserDto(user.Id, user.Email, REGULAR_USER_ROLE));
            }

            //foreach (var visitor in visitors)
            //{
            //    usersList.Add(new UserDto(visitor.Id, visitor.Email, VISITOR_ROLE));
            //}
            
            return usersList;
        }
    }
}
