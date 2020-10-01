using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MyCosts.Controllers.Managment
{
    [Authorize]
    public class Costs : Controller
    {
        private MyCostsContext db;

        public Costs(MyCostsContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await db.Products.LoadAsync();
            await db.Users.LoadAsync();
            return View(await db.Costs.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            int selectedIndex = 1;
            var categories = new SelectList(await db.ProductCategories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name", selectedIndex);
            var products = new SelectList(await db.Products.Where(p => p.CategoryId == selectedIndex).OrderBy(p => p.Name).ToListAsync(), "Id", "Name");
            var addCost = new AddCost
            {
                Categories = categories,
                Products = products
            };
            return View(addCost);
        }

        [HttpGet] // доступно по обращении через путь url, исправить
        public async Task<IActionResult> GetProductsOfACategory(int id)
        {
            var products = await db.Products.Where(p => p.CategoryId == id).OrderBy(p => p.Name).ToListAsync();
            return PartialView(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Cost cost, string userName, string sum, string weight)
        {
            var user = await db.Users.FirstOrDefaultAsync(e => e.UserName == userName);
            cost.User = user;
            cost.Sum = Decimal.Parse(sum, CultureInfo.InvariantCulture);
            if (weight != null) cost.WeightInKg = Double.Parse(weight, CultureInfo.InvariantCulture);
            db.Costs.Add(cost);
            await db.SaveChangesAsync();
            return await Add();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var cost = await db.Costs.FirstOrDefaultAsync(p => p.Id == id);
            if (cost != null)
            {
                db.Costs.Remove(cost);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
