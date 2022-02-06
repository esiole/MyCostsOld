namespace MyCosts.API.Models;

[Serializable]
public class ProductCategoryResponseModel : ProductCategoryModel
{
    [Required]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    public ProductCategoryResponseModel() { }

    public ProductCategoryResponseModel(ProductCategory productCategory)
    {
        Id = productCategory.Id;
        Name = productCategory.Name;
    }

    public static implicit operator ProductCategory(ProductCategoryResponseModel model) => new() 
    { 
        Id = model.Id,
        Name = model.Name 
    };
}
