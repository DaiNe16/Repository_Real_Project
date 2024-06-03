﻿
using Microsoft.EntityFrameworkCore;
using Repository_Real_Project.Data;

namespace Repository_Real_Project.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context) 
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void SetDbContext(AppDbContext dbContext)
        {
            _context = dbContext;
        }
    }
}
