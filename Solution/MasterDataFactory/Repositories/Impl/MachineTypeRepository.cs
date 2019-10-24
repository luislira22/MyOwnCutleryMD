using System;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.MachineTypes;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories.Interfaces;

namespace MasterDataFactory.Repositories.Impl
{
    public class MachineTypeRepository : GenericRepository<MachineType>, IMachineTypeRepository
    {
        public MachineTypeRepository(Context context) : base(context)
        {
            
        }
        public override Task<bool> Exists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}