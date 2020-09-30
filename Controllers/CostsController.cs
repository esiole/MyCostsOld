using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyCosts.Controllers
{
    public class CostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
