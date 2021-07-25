using System;
using System.ComponentModel.DataAnnotations;

namespace MyCosts.Models
{
    public class Cost
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Не выбран продукт")]
        [Display(Name = "Продукт")]
        public int? ProductId { get; set; }

        [Required(ErrorMessage = "Не указана дата")]
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Не указана потраченная сумма")]
        [Display(Name = "Цена")]
        public decimal Sum { get; set; }

        [Display(Name = "Магазин")]
        public string Store { get; set; }

        [Display(Name = "Количество")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Количество купленных товаров не может быть меньше 1")]
        public int? Count { get; set; }

        [Display(Name = "Вес в килограммах")]
        public double? WeightInKg { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
