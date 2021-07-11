using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCosts.Models.Interfaces
{
    public interface IProductsRepository
    {
        public Task AddAsync(Product product);
        public Task DeleteAsync(Product product);
        public Task DeleteAsync(int id);
        public Task<Product> GetProductAsync(int id);
        public Task<IEnumerable<Product>> GetProductsAsync();
        public Task<IEnumerable<Product>> GetProductsAsync(int categoryId);
        public Task UpdateAsync(Product product);
    }
}
