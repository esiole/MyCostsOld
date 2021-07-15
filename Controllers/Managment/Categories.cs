using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCosts.Models;
using MyCosts.Models.Interfaces;
using MyCosts.ViewModels;
using System.Threading.Tasks;

namespace MyCosts.Controllers.Managment
{
    [Authorize]
    public class Categories : Controller
    {
        private const int SizePage = 10;
        private readonly ICategoriesRepository categoriesRepository;

        public Categories(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var skip = (page - 1) * SizePage;
            var categories = await categoriesRepository.GetCategoriesAsync(skip, SizePage);
            var totalCount = await categoriesRepository.CountAsync();
            return View(new Pagination<ProductCategory>
            {
                Records = categories,
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
        public async Task<IActionResult> Add(ProductCategory category)
        {
            await categoriesRepository.AddAsync(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                var category = await categoriesRepository.GetCategoryAsync(id.Value);
                if (category != null)
                    return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategory category)
        {
            await categoriesRepository.UpdateAsync(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await categoriesRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
