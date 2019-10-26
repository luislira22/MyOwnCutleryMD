using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataProduct.PersistenceContext;
using MasterDataProduct.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MasterDataProduct.Repositories.Impl
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public async Task Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public abstract Task<bool> Exists(Guid id);

        public Task<List<TEntity>> GetAll()
        {
            return _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Guid id, TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}