namespace MyCosts.API.Services.ProductCategories;

public interface IProductCategoriesService 
{
    void Add(ProductCategory category);
    List<ProductCategory> Get();
}
