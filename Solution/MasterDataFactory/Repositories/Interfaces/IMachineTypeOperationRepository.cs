using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.MachineTypesOperations;
using MasterDataFactory.Models.Operations;

namespace MasterDataFactory.Repositories.Interfaces
{
    public interface IMachineTypeOperationRepository : IGenericRepository<MachineTypeOperation>
    {
        Task<List<Operation>> GetOperationsByMachineType(Guid machineTypeID);
        Task<List<MachineType>> GetMachineTypesWithOperationId(Guid operationId);
    }

}