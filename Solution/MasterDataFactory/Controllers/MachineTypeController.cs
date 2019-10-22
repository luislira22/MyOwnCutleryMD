using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.MachineTypes;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.PersistenceContext;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineTypeController : ControllerBase
    {
        private readonly Context _context;

        public MachineTypeController(Context context)
        {
            _context = context;

            if (_context.MachineTypes.Count() == 0)
            {
                // Create a new MachineType if collection is empty,
                // which means you can't delete all MachineTypes.(quick bootstrap)
                Operation op = new Operation("Triturar");
                List<Operation> ops = new List<Operation>() {op};
                _context.MachineTypes.Add(new MachineType("Trituradora", ops));
                _context.SaveChanges();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MachineTypeDTO>> GetMachineType(Guid id)
        {
            MachineTypeService machineTypeService = new MachineTypeService(_context);
            MachineType machineType = await machineTypeService.getMachineType(id);
            MachineTypeDTO machinetypeDTO = machineType.toDTO();
            if (machineTypeService == null)
            {
                return NotFound();
            }

            return machinetypeDTO;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineType>>> GetMachineTypes()
        {
            return await new MachineTypeService(_context).GetMachineTypes();
        }

        [HttpPost]
        public async Task<ActionResult<MachineTypeDTO>> PostTodoItem([FromBody] MachineType item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MachineTypeService machineTypeService = new MachineTypeService(_context);
            await machineTypeService.postMachineType(item);
            return CreatedAtAction(nameof(GetMachineType), new {Id = item.Id}, item);
        }

        // GET machinetype/operations/{machineTypeId}
        [HttpGet("operations/{id}")]
        public async Task<ActionResult<IEnumerable<OperationDTO>>> GetOperationsFromMachineType(Guid id)
        {
            try
            {
                var machineType = await new MachineTypeService(_context).getMachineType(id);
                var operationsDTO = machineType.Operations.Select(operation => operation.toDTO()).ToList();
                return operationsDTO.ToList();
            }
            catch (NullReferenceException)
            {
                return NoContent();
            }
        }

        /*

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

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