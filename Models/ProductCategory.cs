using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCosts.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано название категории")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }
    }
}
