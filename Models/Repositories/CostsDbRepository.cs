﻿using Microsoft.EntityFrameworkCore;
using MyCosts.Data;
using MyCosts.Models.Interfaces;
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
            System.Linq.Expressions.Expression<Func<Cost, string>> xxx = c => c.Product.Category.Name;
            var test = db.Costs.GroupBy(c => c.Product.Category.Name).Select(t => new { Date = t.Key, Sum = t.Sum(t => t.Sum) }).OrderByDescending(t => t.Sum).Take(3);
            foreach (var e in test)
            {
                Console.WriteLine(e);
            }
            return await db.Costs.Where(c => c.User == user && start < c.Date && c.Date <= (end ?? DateTime.Now)).SumAsync(c => c.Sum);
        }
    }
}
