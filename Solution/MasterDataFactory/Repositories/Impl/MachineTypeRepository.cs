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

        MachineTypeOperationRepository machineTypeOperationRepository;

        public MachineTypeRepository(Context context) : base(context)
        {
            //context.MachineTypes.Include(m => m.MachineTypeOperations).ToListAsync();
            //context.MachineTypes.Include(m => m.Operations).ToListAsync();
            machineTypeOperationRepository = new MachineTypeOperationRepository(context);
        }
        public override async Task<bool> Exists(Guid id)
        {
            return await _context.MachineTypes.AnyAsync(o => o.Id == id);
        }

        public async Task UpdateWithRelationship(Guid id, MachineType machineType)
        {
            ICollection<MachineTypeOperation> machineTypeOperations = await machineTypeOperationRepository.GetMachineTypeOperationsByMachineType(id);
            foreach (var machineTypeOperation in machineTypeOperations)
            {
                Console.WriteLine(machineTypeOperation);
                await machineTypeOperationRepository.Delete(machineTypeOperation.MachineTypeId, machineTypeOperation.OperationId);
            }
            await Update(id, machineType);
        }
    }
}