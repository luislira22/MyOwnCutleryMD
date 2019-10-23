using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterDataProduct.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task Create(TEntity entity);
        Task Update(Guid id, TEntity entity);
        Task Delete(Guid id);
        Task SaveChangesAsync();
        Task<Boolean> Exists(Guid id);
    }
}