using System;
using System.Threading.Tasks;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MasterDataFactory.Repositories.Impl
{
    public class MachineTypeRepository : GenericRepository<MachineType>, IMachineTypeRepository
    {
        public MachineTypeRepository(Context context) : base(context)
        {
            //context.MachineTypes.Include(m => m.MachineTypeOperations).ToListAsync();
            //context.MachineTypes.Include(m => m.Operations).ToListAsync();
        }
        public override async Task<bool> Exists(Guid id)
        {
            return await _context.MachineTypes.AnyAsync(o => o.Id == id);
        }
    }
}