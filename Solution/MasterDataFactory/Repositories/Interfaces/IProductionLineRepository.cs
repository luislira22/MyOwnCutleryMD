using System;
using System.Threading.Tasks;
using MasterDataFactory.Models.ProductionLines;

namespace MasterDataFactory.Repositories.Interfaces
{
    public interface IProductionLineRepository : IGenericRepository<ProductionLine>
    {
        Task<ProductionLine> GetProductionLineByMachine(Guid machineId);
    }
}