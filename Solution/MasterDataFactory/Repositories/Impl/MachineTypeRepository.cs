using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.MachineTypesOperations;
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
        
        public async Task UpdateWithRelationship(Guid id,MachineType machineType)
        {
            MachineType oldMachineType = await GetById(id);
            MachineTypeOperationRepository machineTypeOperationRepository = new MachineTypeOperationRepository(_context);
            ICollection<MachineTypeOperation> machineTypeOperations = oldMachineType.MachineTypeOperations;
            foreach (var machineTypeOperation in machineTypeOperations)
            {
                await machineTypeOperationRepository.Delete(machineTypeOperation.MachineTypeId, machineTypeOperation.OperationId);
            }
            await Update(id, machineType);
        }
    }
}