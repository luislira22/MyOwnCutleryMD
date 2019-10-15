using System.Threading.Tasks;
using MasterDataFactory.Models.PersistenceContext;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineTypeService
    {
        private readonly Context _context;

        public MachineTypeService(Context context)
        {
            _context = context;
        }

        public async Task<MachineTypeDTO> getMachineType(long id)
        {
            var machineType = await _context.MachineTypes.FindAsync(id);
            return machineType.toDTO();
        }
        public async void postMachineType(MachineType machine)
        {
            _context.MachineTypes.Add(machine);
            await _context.SaveChangesAsync();
        }
    }
}