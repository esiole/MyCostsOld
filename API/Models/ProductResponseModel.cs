namespace MyCosts.API.Models;

[Serializable]
public class ProductResponseModel : ProductModel
{
    [Required]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    public ProductResponseModel() { }

    public ProductResponseModel(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        CategoryId = product.CategoryId ?? 0;
    }

    public static implicit operator Product(ProductResponseModel model) => new() 
    { 
        Id = model.Id,
        Name = model.Name,
        CategoryId = model.CategoryId
    };
}
