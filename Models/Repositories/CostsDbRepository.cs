using Microsoft.EntityFrameworkCore;
using MyCosts.Data;
using MyCosts.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCosts.Models.Repositories
{
    public class CostsDbRepository : ICostsRepository
    {
        private readonly MyCostsDbContext db;

        public CostsDbRepository(MyCostsDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(Cost cost)
        {
            db.Costs.Add(cost);
            await db.SaveChangesAsync();
        }

        public async Task<int> CountAsync() => await db.Costs.CountAsync();

        public async Task<int> CountAsync(User user) => await db.Costs.Where(c => c.User == user).CountAsync();

        public async Task DeleteAsync(Cost cost)
        {
            if (cost != null)
            {
                db.Costs.Remove(cost);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetCostAsync(id));

        public async Task<Cost> GetCostAsync(int id) => await db.Costs.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Cost>> GetCostsAsync()
        {
            return await db.Costs.OrderBy(c => c.Date).ThenBy(c => c.Id).ToListAsync();
        }

        public async Task<IEnumerable<Cost>> GetCostsAsync(User user)
        {
            return await db.Costs.Where(c => c.User == user).OrderBy(c => c.Date).ThenBy(c => c.Id).ToListAsync();
        }

        public async Task<IEnumerable<Cost>> GetCostsAsync(int skip, int take)
        {
            var query = db.Costs.OrderBy(c => c.Date).ThenBy(c => c.Id);
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Cost>> GetCostsAsync(User user, int skip, int take)
        {
            var query = db.Costs.Where(c => c.User == user).OrderBy(c => c.Date).ThenBy(c => c.Id);
            return await query.Skip(skip).Take(take).ToListAsync();
        }
    }
}
