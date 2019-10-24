using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MasterDataFactory.Repositories.Impl
{
    public class MachineRepository : GenericRepository<Machine>, IMachineRepository
    {
        public MachineRepository(Context context) : base(context)
        {
        }

        public override Task<bool> Exists(Guid id)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}