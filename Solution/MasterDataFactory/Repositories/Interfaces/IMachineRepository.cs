using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.Machines;

namespace MasterDataFactory.Repositories.Interfaces
{
    public interface IMachineRepository : IGenericRepository<Machine>
    {
        Task<List<Machine>> GetAllActivatedMachines();
        Task<List<Machine>> GetAllDeactivatedMachines();
        Task<List<Machine>>  GetByType(Guid type);
    }
}