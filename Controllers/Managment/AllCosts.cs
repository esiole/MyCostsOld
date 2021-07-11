using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCosts.Models.Interfaces;
using System.Threading.Tasks;

namespace MyCosts.Controllers.Managment
{
    [Authorize(Roles = "admin")]
    public class AllCosts : Controller
    {
        private readonly ICostsRepository costsRepository;

        public AllCosts(ICostsRepository costsRepository)
        {
            this.costsRepository = costsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await costsRepository.GetCostsAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await costsRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
