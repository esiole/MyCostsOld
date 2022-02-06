namespace MyCosts.API.Models;

[Serializable]
public class ProductCategoryResponseModel : ProductCategoryModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    public ProductCategoryResponseModel() { }

    public ProductCategoryResponseModel(ProductCategory productCategory)
    {
        Id = productCategory.Id;
        Name = productCategory.Name;
    }
}
