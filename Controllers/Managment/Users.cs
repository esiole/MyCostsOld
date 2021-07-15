using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCosts.Models;
using MyCosts.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCosts.Controllers.Managment
{
    [Authorize(Roles = "admin")]
    public class Users : Controller
    {
        private const int SizePage = 10;
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;

        public Users(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var skip = (page - 1) * SizePage;
            var query = userManager.Users.OrderBy(u => u.Email);
            var users = await query.Skip(skip).Take(SizePage).ToListAsync();
            var totalCount = await userManager.Users.CountAsync();
            return View(new Pagination<User>
            {
                Records = users,
                Page = page,
                PerPage = SizePage,
                CountRecords = totalCount
            });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Roles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = await roleManager.Roles.ToListAsync();
                var model = new ChangeRole
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Roles(string userId, List<string> roles)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = await roleManager.Roles.ToListAsync();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);
                await userManager.AddToRolesAsync(user, addedRoles);
                await userManager.RemoveFromRolesAsync(user, removedRoles);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
