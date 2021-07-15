using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCosts.Models.Interfaces
{
    public interface ICategoriesRepository
    {
        public Task AddAsync(ProductCategory category);
        public Task<int> CountAsync();
        public Task<int> CountAsync(string search);
        public Task DeleteAsync(ProductCategory category);
        public Task DeleteAsync(int id);
        public Task<IEnumerable<ProductCategory>> GetCategoriesAsync();
        public Task<IEnumerable<ProductCategory>> GetCategoriesAsync(int skip, int take);
        public Task<IEnumerable<ProductCategory>> GetCategoriesAsync(int skip, int take, string search);
        public Task<ProductCategory> GetCategoryAsync(int id);
        public Task UpdateAsync(ProductCategory category);
    }
}
