namespace MyCosts.API.Models;

[Serializable]
public class ProductCategoryModel
{
    [Required]
    [MaxLength(50)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    public static implicit operator ProductCategory(ProductCategoryModel categoryModel) => new() { Name = categoryModel.Name ?? string.Empty };
}
