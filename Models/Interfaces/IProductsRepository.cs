using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCosts.Models.Interfaces
{
    public interface IProductsRepository
    {
        public Task AddAsync(Product product);
        public Task<int> CountAsync();
        public Task<int> CountAsync(string search);
        public Task DeleteAsync(Product product);
        public Task DeleteAsync(int id);
        public Task<Product> GetProductAsync(int id);
        public Task<IEnumerable<Product>> GetProductsAsync();
        public Task<IEnumerable<Product>> GetProductsAsync(int categoryId);
        public Task<IEnumerable<Product>> GetProductsAsync(int skip, int take);
        public Task<IEnumerable<Product>> GetProductsAsync(int skip, int take, string search);
        public Task UpdateAsync(Product product);
    }
}
