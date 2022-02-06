namespace MyCosts.API.Services.Products;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public void Add(Product product) => _productsRepository.Add(product);
    public List<Product> Get() => _productsRepository.Get();

    public void Remove(int id)
    {
        var product = _productsRepository.Get(id);
        if (product != null) _productsRepository.Remove(product);
    }

    public void Update(Product product) => _productsRepository.Update(product);
}
