using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCosts.Models;
using MyCosts.Models.Interfaces;
using MyCosts.ViewModels;
using System.Threading.Tasks;

namespace MyCosts.Controllers.Managment
{
    [Authorize(Roles = "admin")]
    public class AllCosts : Controller
    {
        private const int SizePage = 50;
        private readonly ICostsRepository costsRepository;

        public AllCosts(ICostsRepository costsRepository)
        {
            this.costsRepository = costsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string search = null)
        {
            var skip = (page - 1) * SizePage;
            var costs = await costsRepository.GetCostsAsync(skip, SizePage, search);
            var totalCount = await costsRepository.CountAsync(search);
            return View(new Pagination<Cost>
            {
                Records = costs,
                Page = page,
                PerPage = SizePage,
                CountRecords = totalCount,
                Search = search
            });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await costsRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
