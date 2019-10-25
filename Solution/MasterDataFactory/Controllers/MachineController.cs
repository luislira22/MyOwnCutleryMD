using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MasterDataFactory.DTO.Machines;
using MasterDataFactory.Models.PersistenceContext;
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
        private readonly IMapper _mapper;

        public MachineController(Context context, IMapper mapper)
        {
            _serviceMachine = new MachineService(context);
            _serviceMachineType = new MachineTypeService(context);
            _mapper = mapper;
        }

        // GET: api/machine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineDTO>>> GetAllMachines()
        {
            //await _serviceMachine.CreateMachine(new Machine(null));
            var machines = await _serviceMachine.GetMachines();
            //var machineDTOList = new Lis;// machines.Value.Select(machine => machine.toDTO()).ToList();
            var machineDTOList = _mapper.Map<List<Machine>, List<MachineDTO>>(machines.Value);

            return machineDTOList;
        }

        // POST: api/machine
        [HttpPost]
        public async Task<ActionResult<MachineDTO>> CreateMachine(MachineDTO machineDTO)
        {
            bool machineTypeExists = await _serviceMachineType.MachineExists(Guid.Parse(machineDTO.MachineType));
            if (!machineTypeExists) return NotFound("Machine type not found.");

            var machineType = await _serviceMachineType.getMachineType(Guid.Parse(machineDTO.MachineType));
            var machineBrand = new MachineBrand(machineDTO.MachineBrand);
            var machineModel = new MachineModel(machineDTO.MachineModel);
            var machineLocation = new MachineLocation(machineDTO.MachineLocation);

            var machine = new Machine(machineType, machineBrand, machineModel, machineLocation);
            await _serviceMachine.CreateMachine(machine);

            var machineCreatedDTO = _mapper.Map<Machine, MachineDTO>(machine);
            return CreatedAtAction("CreateMachine", machineCreatedDTO);
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