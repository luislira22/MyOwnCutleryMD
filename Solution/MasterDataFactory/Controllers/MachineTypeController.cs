using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.DTO;
using MasterDataFactory.DTO.Operations;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Services;
using Microsoft.AspNetCore.Mvc;
using MasterDataFactory.Services;

namespace MasterDataFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineTypeController : ControllerBase
    {
        private readonly MachineTypeService _serviceMachineType;
        private readonly OperationService _serviceOperations;

        public MachineTypeController(Context context)
        {
            _serviceMachineType = new MachineTypeService(context);
            _serviceOperations = new OperationService(context);

            /*  quick bootstrap. Mudar para bootstrapper eventualmente */
            if (context.MachineTypes.Count() == 0)
            {
                //bootstrap();
            }

        }

        //mudar isto para outro sitio depois
        public async Task bootstrap()
        {
            var DTO = new OperationDTO(Guid.NewGuid(), "Triturar", "22:22:11");
            Operation op = new Operation(DTO);
            _serviceOperations.postOperation(op);
            List<Operation> ops = new List<Operation>() { op };
            _serviceMachineType.postMachineType(new MachineType(new MachineTypeDescription("Trituradora"), ops));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MachineTypeDTO>> GetMachineType(Guid id)
        {
            MachineType machineType = await _serviceMachineType.getMachineType(id);
            if (machineType == null)
                return NotFound();
            MachineTypeDTO machinetypeDTO = machineType.toDTO();
            return machinetypeDTO;
        }

        [HttpPost]
        public async Task<ActionResult<MachineTypeDTO>> PostMachineType([FromBody]MachineTypeDTO item)
        {
            List<Operation> operations = new List<Operation>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (OperationDTO opDTO in item.Operations)
            {
                Operation op = await _serviceOperations.getOperationById(opDTO.Id);
                if (op == null)
                {
                    return NotFound(String.Format("The operation with id: {0} was not found!", opDTO.Id));
                }
                operations.Add(op);
            }
            MachineType machineType = new MachineType(new MachineTypeDescription(item.Type), operations);
            await _serviceMachineType.postMachineType(machineType);
            return CreatedAtAction(nameof(GetMachineType), new { Id = machineType.Id }, machineType);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineTypeDTO>>> GetMachineTypes()
        {
            ActionResult<IEnumerable<MachineType>> actionResult = await _serviceMachineType.GetMachineTypes();
            var machines = actionResult.Value.Select(machineType => machineType.toDTO()).ToList();
            return machines;
        }

        // GET machinetype/operations/{machineTypeId}
        [HttpGet("operations/{id}")]
        public async Task<ActionResult<IEnumerable<OperationDTO>>> GetOperationsFromMachineType(Guid id)
        {
            try
            {
                var machineType = await _serviceMachineType.getMachineType(id);
                var operationsDTO = machineType.Operations.Select(operation => operation.toDTO()).ToList();
                return operationsDTO.ToList();
            }
            catch (NullReferenceException)
            {
                return NoContent();
            }
        }

        /*
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)x 
        {
        }
        */
    }
}