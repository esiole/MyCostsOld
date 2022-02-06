namespace MyCosts.API.Models;

[Serializable]
public class ProductModel
{
    [Required]
    [MaxLength(50)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }

    public static implicit operator Product(ProductModel model) => new() 
    { 
        Name = model.Name,
        CategoryId = model.CategoryId,
    };
}
