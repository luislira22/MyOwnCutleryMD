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
            var machines = await _serviceMachine.GetMachines();
            return _mapper.Map<List<Machine>, List<MachineDTO>>(machines.Value);
        }

        // POST: api/machine
        [HttpPost]
        public async Task<ActionResult<MachineDTO>> CreateMachine(MachineDTO machineDTO)
        {
            try
            {
                var machine = await _serviceMachine.CreateMachine(machineDTO);
                return CreatedAtAction("CreateMachine", _mapper.Map<Machine, MachineDTO>(machine));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Machine type not found");
            }
        }
        // DELETE: api/machine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(Guid id)
        {
            try
            {
                await _serviceMachine.DeleteMachine(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Machine not found");
            }
        }

        [HttpGet("machinetype/{type}")]
        //api/machinetype/{type} onde type é o id de um tipo de maquina
        public async Task<ActionResult<MachineDTO>> GetMachineByMachineType(Guid type)
        {
           var machines = await _serviceMachine.GetMachineByType(type);
           return CreatedAtAction("GetMachineByMachineType", _mapper.Map<List<Machine>, List<MachineDTO>>(machines));
        }

        public async Task<ActionResult<MachineDTO>> getMachineById(Guid id)
        {
            bool exists = await _serviceMachine.MachineExists(id);
            if (!exists)
                return NotFound();
            Machine machine = await _serviceMachine.GetMachineById(id);
            var machineCreatedDTO = _mapper.Map<Machine, MachineDTO>(machine);
            return machineCreatedDTO;
        }
    }
}