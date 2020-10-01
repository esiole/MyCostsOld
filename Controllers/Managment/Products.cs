using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MyCosts.Controllers.Managment
{
    [Authorize]
    public class Products : Controller
    {
        private MyCostsContext db;

        public Products(MyCostsContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await db.ProductCategories.LoadAsync();
            return View(await db.Products.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var addProduct = new AddProduct
            {
                Categories = new SelectList(await db.ProductCategories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name"),
            };
            return View(addProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
