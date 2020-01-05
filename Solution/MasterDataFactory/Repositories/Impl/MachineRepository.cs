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
            //context.Machines.Include(m => m.MachineType).ToListAsync();
        }

        public override async Task<bool> Exists(Guid id)
        {
            return await _context.Machines.AnyAsync(o => o.Id == id);
        }

        public async Task<List<Machine>> GetByType(Guid idType)
        {
            List<Machine> machines = await _context.Machines.Where(e => e.MachineType.Id == idType).ToListAsync();
           return machines;
        }
        
        public async Task<List<Machine>> GetAllActivatedMachines()
        {
            List<Machine> machines = 
                await _context.Machines.Where(e => e.MachineState.State == State.Activated).ToListAsync();
            return machines;
        }

        public async Task<List<Machine>> GetAllDeactivatedMachines()
        {
            List<Machine> machines =
                await _context.Machines.Where(e => e.MachineState.State == State.Deactivated).ToListAsync();
            return machines;
        }
    }
}