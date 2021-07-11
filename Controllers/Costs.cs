using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCosts.Models;
using MyCosts.Models.Interfaces;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace MyCosts.Controllers
{
    [Authorize]
    public class Costs : Controller
    {
        private readonly ICostsRepository costsRepository;
        private readonly ICategoriesRepository categoriesRepository;
        private readonly IProductsRepository productsRepository;
        private readonly UserManager<User> userManager;

        public Costs(ICostsRepository costsRepository, ICategoriesRepository categoriesRepository,
            IProductsRepository productsRepository, UserManager<User> userManager)
        {
            this.costsRepository = costsRepository;
            this.categoriesRepository = categoriesRepository;
            this.productsRepository = productsRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            return View(await costsRepository.GetCostsAsync(user));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            int selectedIndex = 1;
            var categories = new SelectList(await categoriesRepository.GetCategoriesAsync(), "Id", "Name", selectedIndex);
            var products = new SelectList(await productsRepository.GetProductsAsync(selectedIndex), "Id", "Name");
            var addCost = new AddCost
            {
                Categories = categories,
                Products = products
            };
            return View(addCost);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsOfACategory(int id)
        {
            var products = await productsRepository.GetProductsAsync(id);
            return PartialView(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Cost cost, string sum, string weight)
        {
            cost.User = await userManager.GetUserAsync(User);
            cost.Sum = Decimal.Parse(sum, CultureInfo.InvariantCulture);
            if (weight != null) cost.WeightInKg = Double.Parse(weight, CultureInfo.InvariantCulture);
            await costsRepository.AddAsync(cost);
            return await Add();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await costsRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
