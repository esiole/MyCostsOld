namespace MyCosts.API.Data.Repositories.Products;

public class ProductsDbRepository : DbRepository<Product>, IProductsRepository
{
    public ProductsDbRepository(MyCostsDbContext context) : base(context) { }

    public override List<Product> Get() => _context.Products.ToList();
}
