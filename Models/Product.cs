using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCosts.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }

        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }

        public Product()
        {
            Costs = new HashSet<Cost>();
        }
    }
}
