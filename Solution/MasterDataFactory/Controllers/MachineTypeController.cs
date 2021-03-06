using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.DTO;
using MasterDataFactory.DTO.MachineType;
using MasterDataFactory.DTO.Operations;
using MasterDataFactory.Helpers;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataFactory.Controllers
{    
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class MachineTypeController : ControllerBase
    {
        private readonly MachineTypeService _serviceMachineType;

        public MachineTypeController(Context context)
        {
            _serviceMachineType = new MachineTypeService(context);

            /*  quick bootstrap. Mudar para bootstrapper eventualmente */
            if (context.MachineTypes.Count() == 0)
            {
                // Create a new MachineType if collection is empty,
                // which means you can't delete all MachineTypes.(quick bootstrap)
                //bootstrap();
            }

        }

        //mudar isto para outro sitio depois
        /*
        public async Task bootstrap()
        {
            var DTO = new OperationDTO(Guid.NewGuid(), "Triturar", "22:22:11");
            Operation op = new Operation(DTO);
            _serviceOperations.postOperation(op);
            List<Operation> ops = new List<Operation>() { op };
            await _serviceMachineType.postMachineType(new MachineType(new MachineTypeDescription("Trituradora"), ops));
        }
        */

        [HttpGet("{id}")]
        public async Task<ActionResult<MachineTypeDTO>> GetMachineType(Guid id)
        {
            try
            {
                MachineType machineType = await _serviceMachineType.getMachineType(id);
                return machineType.toDTO();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(String.Format("The MachineType with id: {0} was not found!", id));
            }
            //catch(SqlException){}
        }

        [HttpPost]
        public async Task<ActionResult<MachineTypeDTO>> PostMachineType([FromBody]MachineTypeDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MachineType machine = await _serviceMachineType.postMachineType(item);
                return CreatedAtAction(nameof(PostMachineType), machine.toDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineTypeDTO>>> GetMachineTypes()
        {
            IEnumerable<MachineType> machines = (await _serviceMachineType.GetMachineTypes()).Value;
            return machines.Select(m => m.toDTO()).ToList();
        }

        // GET machinetype/operations/{machineTypeId}
        [HttpGet("operations/{id}")]
        public async Task<ActionResult<IEnumerable<OperationDTO>>> GetOperationsFromMachineType(Guid id)
        {
            try
            {
                var operations = await _serviceMachineType.getOperations(id);
                return operations.Select(operation => operation.ToDTO()).ToList();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Machine type does not exist");
            }
        }
        
        // PUT machinetype/operations/{machineTypeId}
        [HttpPut("operations/{id}")]
        public async Task<ActionResult<MachineTypeDTO>> UpdateMachineTypeOperations(Guid id,[FromBody]OperationsDTO operationsDTO)
        {
            try
            {
                await _serviceMachineType.UpdateMachineTypeOperation(id, operationsDTO.Operations);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachineType(Guid id)
        {
            try
            {
                await _serviceMachineType.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("MachineType not found");
            }
        }
        
    }
}