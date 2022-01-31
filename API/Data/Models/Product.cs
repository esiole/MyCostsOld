namespace MyCosts.API.Data.Models;

/// <summary>
///     Продукт, товар 
/// </summary>
[Table("Products")]
public class Product
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }
    public ProductCategory? Category { get; set; }

    /// <summary>
    ///     Название товара
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public List<Cost>? Costs { get; set; }
}
