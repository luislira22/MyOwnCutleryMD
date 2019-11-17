using System;
using System.Threading.Tasks;
using MasterDataFactory.Models.MachineTypes;

namespace MasterDataFactory.Repositories.Interfaces
{
    public interface IMachineTypeRepository : IGenericRepository<MachineType>
    {
        Task UpdateWithRelationship(Guid id,MachineType machineType);
    }
    
}