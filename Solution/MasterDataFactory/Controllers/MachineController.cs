using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.DTO;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Models.Domain.MachineTypes;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Services;

namespace MasterDataFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly MachineService _serviceMachine;
        private readonly MachineTypeService _serviceMachineType;

        public MachineController(Context context)
        {
            _serviceMachine = new MachineService(context);
            _serviceMachineType = new MachineTypeService(context);
        }

        // GET: api/machine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineDTO>>> GetAllMachines()
        {
            //await _serviceMachine.CreateMachine(new Machine(null));
            var machines = await _serviceMachine.GetMachines();
            var machineDTOList = machines.Value.Select(machine => machine.toDTO()).ToList();
            return machineDTOList;
        }

        // POST: api/machine
        [HttpPost]
        public async Task<ActionResult<MachineDTO>> CreateMachine(MachineDTO machineDTO)
        {
            bool machineTypeExists = await _serviceMachineType.MachineExists(Guid.Parse(machineDTO.MachineType));
            if(!machineTypeExists) return NotFound("Machine type not found.");
            
            var machineType = await _serviceMachineType.getMachineType(Guid.Parse(machineDTO.MachineType));
            
            var machineBrand = new MachineBrand(machineDTO.MachineBrand);
            var machineModel = new MachineModel(machineDTO.MachineModel);
            MachineLocation machineLocation = new MachineLocation(machineDTO.MachineLocation);
            Machine machine = new Machine(machineType, machineBrand, machineModel, machineLocation);
            Console.WriteLine("----------------------------------------------------" + machine.MachineType.Id);
            await _serviceMachine.CreateMachine(machine);
            return CreatedAtAction("CreateMachine", machine.toDTO());
            //TODO
            //return null;
        }

        // DELETE: api/machine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(Guid id)
        {
            bool exists = await _serviceMachine.MachineExists(id);
            if (!exists) return NotFound();
            await _serviceMachine.DeleteMachine(id);
            return NoContent();
        }
    }
}