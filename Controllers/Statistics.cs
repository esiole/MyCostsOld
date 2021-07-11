using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyCosts.Controllers
{
    [Authorize]
    public class Statistics : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
