using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MyCosts
{
    public class ManagementController : Controller
    {
        private MyCostsContext db;

        public ManagementController(MyCostsContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            return View(await db.ProductCategories.ToListAsync());
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(ProductCategory category)
        {
            db.ProductCategories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Categories");
        }

        //public async Task<IActionResult> EditCategory(int? id)
        //{
        //    if (id != null)
        //    {
        //        var category = await db.ProductCategories.FirstOrDefaultAsync(p => p.Id == id);
        //        if (category != null)
        //            return View(category);
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditCategory(ProductCategory category)
        //{
        //    db.ProductCategories.Update(category);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Categories");
        //}

        [HttpPost]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await db.ProductCategories.FirstOrDefaultAsync(p => p.Id == id);
            if (category != null)
            {
                db.ProductCategories.Remove(category);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            await db.ProductCategories.LoadAsync();
            return View(await db.Products.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var addProduct = new AddProduct
            {
                Categories = new SelectList(await db.ProductCategories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name"),
            };
            return View(addProduct);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Products");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Products");
        }

        [HttpGet]
        public async Task<IActionResult> Costs()
        {
            await db.Products.LoadAsync();
            await db.Users.LoadAsync();
            return View(await db.Costs.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> AddCost()
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

        [HttpGet] // доступно по обращении через путь url
        public async Task<IActionResult> GetProductsOfACategory(int id)
        {
            var products = await db.Products.Where(p => p.CategoryId == id).OrderBy(p => p.Name).ToListAsync();
            return PartialView(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddCost(Cost cost, string userName, string sum, string weight)
        {
            var user = await db.Users.FirstOrDefaultAsync(e => e.UserName == userName);
            cost.User = user;
            cost.Sum = Decimal.Parse(sum, CultureInfo.InvariantCulture);
            if (weight != null) cost.WeightInKg = Double.Parse(weight, CultureInfo.InvariantCulture);
            db.Costs.Add(cost);
            await db.SaveChangesAsync();
            return await AddCost();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCost(int id)
        {
            var cost = await db.Costs.FirstOrDefaultAsync(p => p.Id == id);
            if (cost != null)
            {
                db.Costs.Remove(cost);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Costs");
        }
    }
}
