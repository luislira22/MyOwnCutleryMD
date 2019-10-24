using System;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MasterDataFactory.Repositories.Impl
{
    public class OperationRepository : GenericRepository<Operation>,IOperationRepository
    {
        
        public OperationRepository(Context context) : base(context)
        {
            
        }

        public async override Task<bool> Exists(Guid id)
        {
            return await _context.Operations.AnyAsync(o => o.Id == id);
        }
    }
}