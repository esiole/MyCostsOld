namespace MyCosts.API.Data.Repositories.ProductCategories;

public class ProductCategoriesDbRepository : DbRepository<ProductCategory>, IProductCategoriesRepository
{
    public ProductCategoriesDbRepository(MyCostsDbContext context) : base(context) { }

    public override List<ProductCategory> Get()
    {
        lock (_context.Locker)
        {
            return _context.ProductCategories.ToList();
        }
    }

    public override ProductCategory? Get(int id)
    {
        lock (_context.Locker)
        {
            return _context.ProductCategories.FirstOrDefault(pc => pc.Id == id);
        }
    }
}
