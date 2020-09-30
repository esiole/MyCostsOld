using System;
using System.Collections.Generic;

namespace MyCosts
{
    public class Product
    {
        public Product()
        {
            Costs = new HashSet<Cost>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }

        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
    }
}
