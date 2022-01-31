namespace MyCosts.API.Services.Products;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public List<Product> Get() => _productsRepository.Get();
}
