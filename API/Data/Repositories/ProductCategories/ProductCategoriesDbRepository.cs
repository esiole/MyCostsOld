namespace MyCosts.API.Data.Repositories.ProductCategories;

public class ProductCategoriesDbRepository : DbRepository<ProductCategory>, IProductCategoriesRepository
{
    public ProductCategoriesDbRepository(MyCostsDbContext context) : base(context) { }

    public override List<ProductCategory> Get() => _context.ProductCategories.ToList();
}
