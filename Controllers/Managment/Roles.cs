using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCosts.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MyCosts.Controllers.Managment
{
    [Authorize(Roles = "admin")]
    public class Roles : Controller
    {
        private const int SizePage = 10;
        private RoleManager<IdentityRole> roleManager;

        public Roles(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var skip = (page - 1) * SizePage;
            var query = roleManager.Roles.OrderBy(r => r.Name);
            var roles = await query.Skip(skip).Take(SizePage).ToListAsync();
            var totalCount = await roleManager.Roles.CountAsync();
            return View(new Pagination<IdentityRole>
            {
                Records = roles,
                Page = page,
                PerPage = SizePage,
                CountRecords = totalCount
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
    }
}
