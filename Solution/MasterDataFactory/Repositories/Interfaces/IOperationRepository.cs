using System;
using System.Threading.Tasks;
using MasterDataFactory.Models.Operations;

namespace MasterDataFactory.Repositories.Interfaces
{
    public interface IOperationRepository : IGenericRepository<Operation>
    {
        Task DeleteWithRelationship(Guid operationId);
    }
}