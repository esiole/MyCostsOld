using Microsoft.EntityFrameworkCore;
using MyCosts.Data;
using MyCosts.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCosts.Models.Repositories
{
    public class CategoriesDbRepository : ICategoriesRepository
    {
        private readonly MyCostsDbContext db;

        public CategoriesDbRepository(MyCostsDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(ProductCategory category)
        {
            db.ProductCategories.Add(category);
            await db.SaveChangesAsync();
        }

        public async Task<int> CountAsync() => await db.ProductCategories.CountAsync();

        public async Task DeleteAsync(ProductCategory category)
        {
            if (category != null)
            {
                db.ProductCategories.Remove(category);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetCategoryAsync(id));

        public async Task<IEnumerable<ProductCategory>> GetCategoriesAsync()
        {
            return await db.ProductCategories.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetCategoriesAsync(int skip, int take)
        {
            var query = db.ProductCategories.OrderBy(c => c.Name);
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<ProductCategory> GetCategoryAsync(int id) => await db.ProductCategories.FirstOrDefaultAsync(с => с.Id == id);

        public async Task UpdateAsync(ProductCategory category)
        {
            db.ProductCategories.Update(category);
            await db.SaveChangesAsync();
        }
    }
}
