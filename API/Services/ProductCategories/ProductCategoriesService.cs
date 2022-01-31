namespace MyCosts.API.Services.ProductCategories;

public class ProductCategoriesService : IProductCategoriesService
{
    private readonly IProductCategoriesRepository _productCategoriesRepository;

    public ProductCategoriesService(IProductCategoriesRepository productCategoriesRepository)
    {
        _productCategoriesRepository = productCategoriesRepository;
    }
}
