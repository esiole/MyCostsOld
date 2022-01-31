namespace MyCosts.API.Data.Models;

/// <summary>
///     Категория товара
/// </summary>
/// <remarks>
///     Например, еда, одежда, лекарства и т.д.
/// </remarks>
[Table("ProductCategories")]
public class ProductCategory
{
    public int Id { get; set; }

    /// <summary>
    ///     Название категории
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public List<Product>? Products { get; set; }
}
