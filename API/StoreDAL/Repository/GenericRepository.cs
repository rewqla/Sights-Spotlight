using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly StoreContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(StoreContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<TEntity> FindById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.AddAsync(entity);
            }
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
