namespace MyCosts.API.Data.Repositories.Products;

public class ProductsDbRepository : Repository<Product>, IProductsRepository
{
    public ProductsDbRepository(MyCostsDbContext context) : base(context) { }
}
