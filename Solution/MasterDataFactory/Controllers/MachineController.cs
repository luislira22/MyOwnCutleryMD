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
        private readonly IMapper _mapper;

        public MachineController(Context context, IMapper mapper)
        {
            _serviceMachine = new MachineService(context);
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
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var machine = await _serviceMachine.CreateMachine(machineDTO);
                return CreatedAtAction("CreateMachine", _mapper.Map<Machine, MachineDTO>(machine));
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(exception.Message);
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
            try
            {
                var machines = await _serviceMachine.GetMachineByType(type);
                return CreatedAtAction("GetMachineByMachineType",
                    _mapper.Map<List<Machine>, List<MachineDTO>>(machines));
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MachineDTO>> GetMachineById(Guid id)
        {
            try
            {
                Machine machine = await _serviceMachine.GetMachineById(id);
                var machineCreatedDTO = _mapper.Map<Machine, MachineDTO>(machine);
                return machineCreatedDTO;
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MachineDTO>> changeMachineType(Guid id, [FromBody]string newMachineTypeId)
        {
            try
            {
                if (newMachineTypeId.Length==0)
                {
                    return BadRequest("Machine object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _serviceMachine.UpdateMachineType(id, newMachineTypeId);
                Machine machine = await _serviceMachine.GetMachineById(id);
                return _mapper.Map<Machine, MachineDTO>(machine);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            /*Acrescentar mais tarde
            catch(Exception ex){
                return StatusCode(500, "Internal server error");
            }*/
        }
    }
}