using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.PersistenceContext;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class MachineService
    {
        private readonly Context _context;

        public MachineService(Context context) {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Machine>>> GetMachines()
        {
            return await _context.Machines.ToListAsync();
        }

        public async Task<ActionResult<Machine>> CreateMachine(Machine machine)
        {
            //TODO return
            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();
            return null;
        } 
    }
}