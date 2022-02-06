namespace MyCosts.API.Services.Products;

public interface IProductsService 
{
    void Add(Product product);
    List<Product> Get();
    void Remove(int id);
    void Update(Product product);
}
