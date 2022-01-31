namespace MyCosts.API.Data.Models;

/// <summary>
///     Расходы пользователя, которые являются позициями в чеке
/// </summary>
[Table("Costs")]
public class Cost
{
    public int Id { get; set; }

    public int? ProductId { get; set; }
    public Product? Product { get; set; }

    public int? BillId { get; set; }
    public Bill? Bill { get; set; }

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }

    /// <summary>
    ///     Цена единицы продукта
    /// </summary>
    [Required]
    public decimal Sum { get; set; }

    /// <summary>
    ///     Количество единиц продукта
    /// </summary>
    [Required]
    public int Count { get; set; }

    /// <summary>
    ///     Вес единицы продукта в кг
    /// </summary>
    public double? WeightInKg { get; set; }
}
