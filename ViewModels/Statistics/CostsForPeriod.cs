using System;
using System.Collections.Generic;

namespace MyCosts.ViewModels.Statistics
{
    public class CostsForPeriod
    {
        public IEnumerable<CostsGroupByCategory> CostsByCategory { get; set; }
        public decimal Sum { get; set; }
        public decimal PreviousPeriodSum { get; set; }
        public bool IsCostsLower { get => Sum < PreviousPeriodSum; }
        public decimal DeltaSum { get => Math.Abs(Sum - PreviousPeriodSum); }
    }
}
