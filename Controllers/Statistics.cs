using ChartJSCore.Helpers;
using ChartJSCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCosts.Extensions;
using MyCosts.Models;
using MyCosts.Models.Interfaces;
using MyCosts.ViewModels.Statistics;
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
        private readonly Random random;

        public Color NextColor => Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

        public Statistics(ICostsRepository costsRepository, UserManager<User> userManager)
        {
            this.costsRepository = costsRepository;
            this.userManager = userManager;
            random = new Random();
        }

        [HttpGet]
        public async Task<IActionResult> Index(GetStatistic getStatistic = null)
        {
            if (getStatistic.Start == default || getStatistic.End == default)
            {
                getStatistic =  new GetStatistic { Start = DateTime.Now.AddYears(-1), End = DateTime.Now };
            }
            else if (!ModelState.IsValid)
            {
                return View(getStatistic);
            }
            var user = await userManager.GetUserAsync(User);

            getStatistic.SumCosts = await costsRepository.GetSumCostsAsync(user, getStatistic.Start, getStatistic.End);

            var groupCostsByProduct = await costsRepository.GroupCostsByProductAsync(user, getStatistic.Start, getStatistic.End, take: 20);
            List<string> productLabels = new();
            ChartJSDataset productsDataset = new("Расходы за период");
            foreach (var cbp in groupCostsByProduct)
            {
                productLabels.Add(cbp.GroupName);
                productsDataset.Add(Convert.ToDouble(cbp.Sum), ChartColor.FromHexString(NextColor.ToHexString()));
            }

            var groupCostsByCategory = await costsRepository.GroupCostsByCategoryAsync(user, getStatistic.Start, getStatistic.End);
            List<string> categoryLabels = new();
            ChartJSDataset costsDataset = new("Расходы по категориям");
            foreach (var cbc in groupCostsByCategory)
            {
                categoryLabels.Add(cbc.GroupName);
                costsDataset.Add(Convert.ToDouble(cbc.Sum), ChartColor.FromHexString(NextColor.ToHexString()));
            }

            List<string> monthLabels = new();
            ChartJSDataset costsSumDataset = new("Расходы за месяц", NextColor);
            List<string> top5ProductNames = new() { productLabels[0], productLabels[1], productLabels[2], productLabels[3], productLabels[4] };
            var top5ProductsDataset = new ChartJSDataset[5];
            for (int i = 0; i < top5ProductsDataset.Length; i++)
            {
                top5ProductsDataset[i] = new ChartJSDataset(top5ProductNames[i], NextColor);
            }
            DateTime tempDate = getStatistic.Start;
            while (tempDate < getStatistic.End)
            {
                monthLabels.Add(tempDate.ToString("MMMM") + $" {tempDate.Year}");
                decimal sumCostsPerMonth = await costsRepository.GetSumCostsPerMonthAsync(user, tempDate);
                costsSumDataset.AddValue(Convert.ToDouble(sumCostsPerMonth));

                for (int i = 0; i < top5ProductsDataset.Length; i++)
                {
                    decimal productSumPerMonth = await costsRepository.GetSumCostsPerMonthAsync(user, top5ProductNames[i], tempDate);
                    top5ProductsDataset[i].AddValue(Convert.ToDouble(productSumPerMonth));
                }

                tempDate = tempDate.AddMonths(1);
            }

            ViewData["productsBarChart"] = GenerateBarChart(productsDataset, productLabels);
            ViewData["categoriesPieChart"] = GeneratePieChart(costsDataset, categoryLabels);
            ViewData["costsLineChart"] = GenerateLineChart(costsSumDataset, monthLabels);
            ViewData["topProductsLineChart"] = GenerateLineChart(top5ProductsDataset, monthLabels);
            getStatistic.IsCompleteStatistic = true;

            return View(getStatistic);
        }

        [NonAction]
        public static Chart GenerateBarChart(ChartJSDataset chartDataset, List<string> labels)
        {
            BarDataset dataset = new()
            {
                Label = chartDataset.Title,
                Data = chartDataset.Values,
                BackgroundColor = chartDataset.Colors,
                BorderColor = chartDataset.Colors,
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
        public static Chart GeneratePieChart(ChartJSDataset chartDataset, List<string> labels)
        {
            PieDataset dataset = new()
            {
                Label = chartDataset.Title,
                BackgroundColor = chartDataset.Colors,
                HoverBackgroundColor = chartDataset.Colors,
                Data = chartDataset.Values
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
        public static Chart GenerateLineChart(ChartJSDataset chartDataset, List<string> labels)
        {
            return GenerateLineChart(new[] { chartDataset }, labels);
        }

        [NonAction]
        public static Chart GenerateLineChart(ChartJSDataset[] chartDatasets, List<string> labels)
        {
            ChartJSCore.Models.Data data = new()
            {
                Labels = labels,
                Datasets = new List<Dataset>()
            };

            foreach (ChartJSDataset dataset in chartDatasets)
            {
                data.Datasets.Add(new LineDataset
                {
                    Label = dataset.Title,
                    Data = dataset.Values,
                    Fill = "false",
                    LineTension = 0.1,
                    BackgroundColor = ChartColor.FromRgba(dataset.Color.R, dataset.Color.G, dataset.Color.B, 0.4),
                    BorderColor = ChartColor.FromRgba(dataset.Color.R, dataset.Color.G, dataset.Color.B, 1),
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
                });
            }

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
