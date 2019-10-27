using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.MachineTypesOperations;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories.Interfaces;

namespace MasterDataFactory.Repositories.Impl
{
    public class MachineTypeOperationRepository : GenericRepository<MachineTypeOperation>, IMachineTypeOperationRepository
    {
        public MachineTypeOperationRepository(Context context) : base(context)
        {
        }

        public override Task<bool> Exists(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Operation>> GetOperationsByMachineType(Guid machineTypeID)
        {
            List<MachineTypeOperation> MachineTypeOperation = new List<MachineTypeOperation>(_context.MachineTypeOperations.Where(e => e.MachineType.Id == machineTypeID));
            return MachineTypeOperation.Select(mo => mo.Operation).ToList();
        }

        public async Task<List<MachineType>> GetMachineTypesWithOperationId(Guid operationId)
        {
            List<MachineTypeOperation> MachineTypeOperation = new List<MachineTypeOperation>(_context.MachineTypeOperations.Where(e => e.Operation.Id == operationId));
            return MachineTypeOperation.Select(mo => mo.MachineType).ToList();
        }
    }
}