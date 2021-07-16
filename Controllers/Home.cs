﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCosts.Models;
using MyCosts.Models.Interfaces;
using MyCosts.ViewModels;

namespace MyCosts.Controllers
{
    [Authorize]
    public class Home : Controller
    {
        private readonly ICostsRepository costsRepository;
        private readonly UserManager<User> userManager;
        private readonly ILogger<Home> _logger;

        public Home(ICostsRepository costsRepository, UserManager<User> userManager, ILogger<Home> logger)
        {
            this.costsRepository = costsRepository;
            this.userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            System.Console.WriteLine(user == null);
            return View(new UserHome
            {
                Last30DaysSumCosts = await costsRepository.GetSumCostsAsync(user, DateTime.Now.AddDays(-30)),
                CurrentMonthSumCosts = await costsRepository.GetSumCostsAsync(user, DateTime.Now.AddDays(-DateTime.Now.Day)),
                YearSumCosts = await costsRepository.GetSumCostsAsync(user, DateTime.Now.AddYears(-1))
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
