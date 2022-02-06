namespace MyCosts.API.Data.Repositories.Products;

public class ProductsDbRepository : DbRepository<Product>, IProductsRepository
{
    public ProductsDbRepository(MyCostsDbContext context) : base(context) { }

    public override List<Product> Get()
    {
        lock (_context.Locker)
        {
            return _context.Products.ToList();
        }
    }

    public override Product? Get(int id)
    {
        lock (_context.Locker)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
