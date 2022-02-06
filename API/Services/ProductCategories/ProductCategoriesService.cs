namespace MyCosts.API.Services.ProductCategories;

public class ProductCategoriesService : IProductCategoriesService
{
    private readonly IProductCategoriesRepository _productCategoriesRepository;

    public ProductCategoriesService(IProductCategoriesRepository productCategoriesRepository)
    {
        _productCategoriesRepository = productCategoriesRepository;
    }

    public void Add(ProductCategory category) => _productCategoriesRepository.Add(category);
    public List<ProductCategory> Get() => _productCategoriesRepository.Get();

    public void Remove(int id)
    {
        var productCategory = _productCategoriesRepository.Get(id);
        if (productCategory != null) _productCategoriesRepository.Remove(productCategory);
    }

    public void Update(ProductCategory category) => _productCategoriesRepository.Update(category);
}
