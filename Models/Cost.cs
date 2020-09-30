using System;
using System.Collections.Generic;

namespace MyCosts
{
    public class Cost
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? ProductId { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string Store { get; set; }
        public int? Count { get; set; }
        public double? WeightInKg { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
