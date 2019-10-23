using System;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.PersistenceContext;

namespace MasterDataFactory.Repositories
{
    public class OperationRepository : GenericRepository<Operation>,IOperationRepository
    {
        
        public OperationRepository(Context context) : base(context)
        {
            
        }

        public override Task<bool> Exists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}