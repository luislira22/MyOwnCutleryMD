using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.ProductionLines;

namespace MasterDataFactory.Repositories.Interfaces
{
    public interface IProductionLineRepository : IGenericRepository<ProductionLine>
    {
        Task<List<ProductionLine>> GetProductionLineByMachine(Guid machineId);
    }
}