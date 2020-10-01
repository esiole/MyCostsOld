using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyCosts.Controllers.Managment
{
    [Authorize]
    public class Categories : Controller
    {
        private MyCostsContext db;

        public Categories(MyCostsContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await db.ProductCategories.ToListAsync());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductCategory category)
        {
            db.ProductCategories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
        public async Task<ActionResult> Delete(int id)
        {
            var category = await db.ProductCategories.FirstOrDefaultAsync(p => p.Id == id);
            if (category != null)
            {
                db.ProductCategories.Remove(category);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
