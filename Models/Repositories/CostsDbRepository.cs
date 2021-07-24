using Microsoft.EntityFrameworkCore;
using MyCosts.Data;
using MyCosts.Models.Interfaces;
using MyCosts.ViewModels.Statistics;
using System;
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

        public async Task<int> CountAsync(string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return await CountAsync();
            }
            
            var count = await db.Costs.Where(c => c.Product.Name.Contains(search) || c.Product.Category.Name.Contains(search) ||
                                                    c.Store.Contains(search) || c.User.Email.Contains(search) || 
                                                    c.Date.ToString().Contains(search) || c.Sum.ToString().Contains(search) ||
                                                    c.Count.ToString().Contains(search) || c.WeightInKg.ToString().Contains(search))
                                        .CountAsync();
            return count;
        }

        public async Task<int> CountAsync(User user, string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return await CountAsync(user);
            }

            var count = await db.Costs.Where(c => c.User == user && 
                                                    (c.Product.Name.Contains(search) || c.Product.Category.Name.Contains(search) ||
                                                    c.Store.Contains(search) || c.User.Email.Contains(search) ||
                                                    c.Date.ToString().Contains(search) || c.Sum.ToString().Contains(search) ||
                                                    c.Count.ToString().Contains(search) || c.WeightInKg.ToString().Contains(search)))
                                        .CountAsync();
            return count;
        }

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
            return await db.Costs.OrderByDescending(c => c.Date).ThenBy(c => c.Id).ToListAsync();
        }

        public async Task<IEnumerable<Cost>> GetCostsAsync(User user)
        {
            return await db.Costs.Where(c => c.User == user).OrderByDescending(c => c.Date).ThenBy(c => c.Id).ToListAsync();
        }

        public async Task<IEnumerable<Cost>> GetCostsAsync(int skip, int take)
        {
            var query = db.Costs.OrderByDescending(c => c.Date).ThenBy(c => c.Id);
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Cost>> GetCostsAsync(User user, int skip, int take)
        {
            var query = db.Costs.Where(c => c.User == user).OrderByDescending(c => c.Date).ThenBy(c => c.Id);
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Cost>> GetCostsAsync(int skip, int take, string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return await GetCostsAsync(skip, take);
            }
            var query = db.Costs.Where(c => c.Product.Name.Contains(search) || c.Product.Category.Name.Contains(search) ||
                                            c.Store.Contains(search) || c.User.Email.Contains(search) ||
                                            c.Date.ToString().Contains(search) || c.Sum.ToString().Contains(search) ||
                                            c.Count.ToString().Contains(search) || c.WeightInKg.ToString().Contains(search))
                                .OrderByDescending(c => c.Date).ThenBy(c => c.Id);
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Cost>> GetCostsAsync(User user, int skip, int take, string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return await GetCostsAsync(user, skip, take);
            }
            var query = db.Costs.Where(c => c.User == user && 
                                            (c.Product.Name.Contains(search) || c.Product.Category.Name.Contains(search) ||
                                            c.Store.Contains(search) || c.User.Email.Contains(search) ||
                                            c.Date.ToString().Contains(search) || c.Sum.ToString().Contains(search) ||
                                            c.Count.ToString().Contains(search) || c.WeightInKg.ToString().Contains(search)))
                                .OrderByDescending(c => c.Date).ThenBy(c => c.Id);
            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<decimal> GetSumCostsAsync(User user, DateTime start, DateTime? end = null)
        {
            return await db.Costs.Where(c => c.User == user && start < c.Date && c.Date <= (end ?? DateTime.Now)).SumAsync(c => c.Sum);
        }

        public async Task<decimal> GetSumCostsPerMonthAsync(User user, DateTime month)
        {
            return await db.Costs.Where(c => c.User == user && c.Date.Year == month.Year && c.Date.Month == month.Month).SumAsync(c => c.Sum);
        }

        public async Task<decimal> GetSumCostsPerMonthAsync(User user, string productName, DateTime month)
        {
            return await db.Costs
                .Where(c => c.User == user && c.Product.Name == productName && c.Date.Year == month.Year && c.Date.Month == month.Month)
                .SumAsync(c => c.Sum);
        }

        public async Task<IEnumerable<CostsGroupBy>> GroupCostsByCategoryAsync(User user, DateTime start, DateTime? end = null, int? take = null)
        {
            var query = db.Costs.Where(c => c.User == user && start < c.Date && c.Date <= (end ?? DateTime.Now))
                .GroupBy(c => c.Product.Category.Name)
                .Select(g => new CostsGroupBy { GroupName = g.Key, Sum = g.Sum(c => c.Sum) })
                .OrderByDescending(g => g.Sum);
            if (take.HasValue)
            {
                return await query.Take(take.Value).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<CostsGroupBy>> GroupCostsByProductAsync(User user, DateTime start, DateTime? end = null, int? take = null)
        {
            var query = db.Costs.Where(c => c.User == user && start < c.Date && c.Date <= (end ?? DateTime.Now))
                .GroupBy(c => c.Product.Name)
                .Select(g => new CostsGroupBy { GroupName = g.Key, Sum = g.Sum(c => c.Sum) })
                .OrderByDescending(g => g.Sum);
            if (take.HasValue)
            {
                return await query.Take(take.Value).ToListAsync();
            }
            return await query.ToListAsync();
        }
    }
}
