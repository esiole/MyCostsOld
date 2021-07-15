using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCosts.Models.Interfaces
{
    public interface ICostsRepository
    {
        public Task AddAsync(Cost cost);
        public Task<int> CountAsync();
        public Task<int> CountAsync(User user);
        public Task DeleteAsync(Cost cost);
        public Task DeleteAsync(int id);
        public Task<Cost> GetCostAsync(int id);
        public Task<IEnumerable<Cost>> GetCostsAsync();
        public Task<IEnumerable<Cost>> GetCostsAsync(User user);
        public Task<IEnumerable<Cost>> GetCostsAsync(int skip, int take);
        public Task<IEnumerable<Cost>> GetCostsAsync(User user, int skip, int take);
    }
}
