using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Machines;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Models.Domain.MachineTypes;
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
        public async Task<ActionResult<IEnumerable<Machine>>> GetTodoItems()
        {
            return await _serviceMachine.GetMachines();
        }

        // POST: api/machine
        [HttpPost]
        public async Task<ActionResult<Machine>> CreateMachineAndAddMachineType(MachineTypeDTO machineTypeDTO)
        {
            var machineType = await _serviceMachineType.getMachineType(machineTypeDTO.Id);
            if (machineType == null) return NotFound("Machine type not found.");
            var machine = new Machine(machineType);
            await _serviceMachine.CreateMachine(machine);
            return CreatedAtAction("CreateMachineAndAddMachineType", machine.toDTO());
        }
    }
}