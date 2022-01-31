namespace MyCosts.API.Data.Repositories.ProductCategories;

public class ProductCategoriesDbRepository : Repository<Product>, IProductCategoriesRepository
{
    public ProductCategoriesDbRepository(MyCostsDbContext context) : base(context) { }
}
