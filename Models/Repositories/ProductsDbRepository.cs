using Microsoft.EntityFrameworkCore;
using MyCosts.Data;
using MyCosts.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCosts.Models.Repositories
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly MyCostsDbContext db;

        public ProductsDbRepository(MyCostsDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
        }

        public async Task<int> CountAsync() => await db.Products.CountAsync();

        public async Task<int> CountAsync(string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return await CountAsync();
            }
            return await db.Products.Where(p => p.Name.Contains(search) || p.Category.Name.Contains(search)).CountAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            db.Products.Remove(product);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetProductAsync(id));

        public async Task<Product> GetProductAsync(int id) => await db.Products.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await db.Products.OrderBy(p => p.Category.Name).ThenBy(p => p.Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int categoryId)
        {
            return await db.Products.Where(p => p.CategoryId == categoryId).OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int skip, int take)
        {
            var query = db.Products.OrderBy(p => p.Category.Name).ThenBy(p => p.Name);
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int skip, int take, string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return await GetProductsAsync(skip, take);
            }
            var query = db.Products.Where(p => p.Name.Contains(search) || p.Category.Name.Contains(search))
                                    .OrderBy(p => p.Category.Name).ThenBy(p => p.Name);
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
        }
    }
}
