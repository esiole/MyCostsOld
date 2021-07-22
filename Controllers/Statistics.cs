using ChartJSCore.Helpers;
using ChartJSCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCosts.Extensions;
using MyCosts.Models;
using MyCosts.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace MyCosts.Controllers
{
    [Authorize]
    public class Statistics : Controller
    {
        private readonly ICostsRepository costsRepository;
        private readonly UserManager<User> userManager;

        public Statistics(ICostsRepository costsRepository, UserManager<User> userManager)
        {
            this.costsRepository = costsRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DateTime start = DateTime.Now.AddYears(-1);
            DateTime end = DateTime.Now;
            if (start > end) throw new ArgumentException("Дата start не может быть больше, чем дата end");

            var user = await userManager.GetUserAsync(User);
            Random random = new();

            var groupCostsByProduct = await costsRepository.GroupCostsByProductAsync(user, start, take: 30);
            List<string> products = new();
            List<double?> costsByProducts = new();
            List<ChartColor> colorsForBar = new();
            foreach (var cbp in groupCostsByProduct)
            {
                products.Add(cbp.GroupName);
                costsByProducts.Add(Convert.ToDouble(cbp.Sum));

                Color randColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                colorsForBar.Add(ChartColor.FromHexString(randColor.ToHexString()));
            }

            var groupCostsByCategory = await costsRepository.GroupCostsByCategoryAsync(user, start);

            List<string> categories = new();
            List<double?> costsByCategories = new();
            List<ChartColor> colorsForPie = new();
            foreach (var cbc in groupCostsByCategory)
            {
                categories.Add(cbc.GroupName);
                costsByCategories.Add(Convert.ToDouble(cbc.Sum));

                Color randColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                colorsForPie.Add(ChartColor.FromHexString(randColor.ToHexString()));
            }

            List<string> months = new();
            List<double?> costsByMonths = new();
            DateTime temp = start;
            while (temp < end)
            {
                decimal sum = await costsRepository.GetSumCostsPerMonthAsync(user, temp);
                months.Add(temp.ToString("MMMM") + $" {temp.Year}");
                costsByMonths.Add(Convert.ToDouble(sum));
                temp = temp.AddMonths(1);
            }

            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            ViewData["barChart"] = GenerateBarChart("Расходы по продуктам", costsByProducts, products, colorsForBar);
            ViewData["pieChart"] = GeneratePieChart("Расходы по категориям", costsByCategories, categories, colorsForPie);
            ViewData["lineChart"] = GenerateLineChart("Расходы за месяц", costsByMonths, months, randomColor);

            return View();
        }

        [NonAction]
        public static Chart GenerateBarChart(string title, List<double?> values, List<string> labels, List<ChartColor> colors)
        {
            BarDataset dataset = new()
            {
                Label = title,
                Data = values,
                BackgroundColor = colors,
                BorderColor = colors,
                BorderWidth = new List<int>() { 1 }
            };

            ChartJSCore.Models.Data data = new()
            {
                Labels = labels,
                Datasets = new List<Dataset>()
            };
            data.Datasets.Add(dataset);

            var options = new Options
            {
                Scales = new Scales
                {
                    YAxes = new List<Scale>
                    {
                        new CartesianScale
                        {
                            Ticks = new CartesianLinearTick
                            {
                                BeginAtZero = true
                            }
                        }
                    },
                    XAxes = new List<Scale>
                    {
                        new BarScale
                        {
                            BarPercentage = 0.5,
                            BarThickness = 6,
                            MaxBarThickness = 8,
                            MinBarLength = 2,
                            GridLines = new GridLine()
                            {
                                OffsetGridLines = true
                            }
                        }
                    }
                },
                Layout = new Layout
                {
                    Padding = new Padding
                    {
                        PaddingObject = new PaddingObject
                        {
                            Left = 10,
                            Right = 12
                        }
                    }
                }
            };

            return new Chart
            {
                Type = Enums.ChartType.Bar,
                Data = data,
                Options = options
            };
        }

        [NonAction]
        public static Chart GeneratePieChart(string title, List<double?> values, List<string> labels, List<ChartColor> colors)
        {
            PieDataset dataset = new()
            {
                Label = title,
                BackgroundColor = colors,
                HoverBackgroundColor = colors,
                Data = values
            };

            ChartJSCore.Models.Data data = new()
            {
                Labels = labels,
                Datasets = new List<Dataset>()
            };
            data.Datasets.Add(dataset);

            return new Chart
            {
                Type = Enums.ChartType.Pie,
                Data = data
            };
        }


        [NonAction]
        public static Chart GenerateLineChart(string title, List<double?> values, List<string> labels, Color? color = null)
        {
            color ??= Color.IndianRed;
            LineDataset dataset = new()
            {
                Label = title,
                Data = values,
                Fill = "false",
                LineTension = 0.1,
                BackgroundColor = ChartColor.FromRgba(color.Value.R, color.Value.G, color.Value.B, 0.4),
                BorderColor = ChartColor.FromRgba(color.Value.R, color.Value.G, color.Value.B, 1),
                BorderCapStyle = "butt",
                BorderDash = new List<int> { },
                BorderDashOffset = 0.0,
                BorderJoinStyle = "miter",
                PointBorderWidth = new List<int> { 1 },
                PointHoverRadius = new List<int> { 5 },
                PointHoverBorderWidth = new List<int> { 2 },
                PointRadius = new List<int> { 1 },
                PointHitRadius = new List<int> { 10 },
                SpanGaps = false
            };

            ChartJSCore.Models.Data data = new()
            {
                Labels = labels,
                Datasets = new List<Dataset>()
            };
            data.Datasets.Add(dataset);

            Options options = new()
            {
                Scales = new Scales()
                {
                    YAxes = new List<Scale>()
                    {
                        new CartesianScale()
                        {
                            Ticks = new Tick()
                            {
                                Callback = "function(value, index, values) {return value + ' ₽';}"
                            }
                        }
                    }
                }
            };

            return new Chart
            {
                Type = Enums.ChartType.Line,
                Data = data,
                Options = options
            };
        }
    }
}
