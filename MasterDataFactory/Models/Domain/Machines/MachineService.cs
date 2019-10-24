using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.PersistenceContext;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class MachineService
    {
        private readonly MachineContext _context;

        public MachineService(MachineContext context) {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Machine>>> GetMachines()
        {
            return await _context.Machines.ToListAsync();
        }

        public async Task<ActionResult<Machine>> CreateMachine(Machine machine)
        {   
            /*MachineId machineId = new MachineId(1);
            _context.MachineIds.Add(machineId);
            machine.MachineId = machineId;*/
            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();
            //return CreatedAtAction(nameof(machine), new { id = machine.Id,  }, machine);
            return null;
        } 
    }
}