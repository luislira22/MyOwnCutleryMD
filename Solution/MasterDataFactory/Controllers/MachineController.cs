using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Machines;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Models.Domain.MachineTypes;

namespace MasterDataFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly MachineService _serviceMachineType;
        private readonly MachineTypeService _serviceMachineTypeService;

        public MachineController(Context context)
        {
            _serviceMachineType = new MachineService(context);
            _serviceMachineTypeService = new MachineTypeService(context);
            //context.AddAsync(new Machine());
        }

        // GET: api/machine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Machine>>> GetTodoItems()
        {
            return await _serviceMachineType.GetMachines();
        }

        // POST: api/machine
        [HttpPost]
        public async Task<ActionResult<Machine>> CreateMachineAndAddMachineType(MachineTypeDTO machineTypeDTO)
        {
            MachineType machineType = await _serviceMachineTypeService.getMachineType(machineTypeDTO.Id);
            if (machineType == null) return NotFound("Machine type not found.");
            var machine = new Machine(machineType);
            await _serviceMachineType.CreateMachine(machine);
            return CreatedAtAction("CreateMachineAndAddMachineType", machine.toDTO());
        }
    }
}