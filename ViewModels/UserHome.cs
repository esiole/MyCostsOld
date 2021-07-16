using MyCosts.ViewModels.Statistics;

namespace MyCosts.ViewModels
{
    public class UserHome
    {
        public CostsForPeriod CurrentMonthSumCosts { get; set; }
        public CostsForPeriod Last30DaysSumCosts { get; set; }
        public CostsForPeriod YearSumCosts { get; set; }
    }
}
