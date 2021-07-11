using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCosts.Models;
using MyCosts.Models.Interfaces;
using System.Threading.Tasks;

namespace MyCosts.Controllers.Managment
{
    [Authorize]
    public class Products : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly ICategoriesRepository categoriesRepository;

        public Products(IProductsRepository productsRepository, ICategoriesRepository categoriesRepository)
        {
            this.productsRepository = productsRepository;
            this.categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await productsRepository.GetProductsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var addProduct = new AddProduct
            {
                Categories = new SelectList(await categoriesRepository.GetCategoriesAsync(), "Id", "Name"),
            };
            return View(addProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            await productsRepository.AddAsync(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                var product = await productsRepository.GetProductAsync(id.Value);
                if (product != null)
                {
                    var editProduct = new AddProduct
                    {
                        Product = product,
                        Categories = new SelectList(await categoriesRepository.GetCategoriesAsync(), "Id", "Name")
                    };
                    return View(editProduct);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            await productsRepository.UpdateAsync(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await productsRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
