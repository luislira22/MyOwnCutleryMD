using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.PersistenceContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineTypeService
    {
        private readonly Context _context;

        public MachineTypeService(Context context)
        {
            _context = context;
        }
        
        public async Task<bool> doesMachineExists(Guid id)
        {
            //TODO implementar reposotorios e deixar a linha de baixo
            //return await _machineRepository.Exists(id);
            return true;
        }
        public async Task<MachineType> getMachineType(Guid id)
        {
            try
            {
                var machineType = await _context.MachineTypes.FindAsync(id);
                return machineType;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task postMachineType(MachineType machine)
        {
            _context.MachineTypes.Add(machine);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<MachineType>>> GetMachineTypes()
        {
            return await _context.MachineTypes.ToListAsync();
        }
    }
}