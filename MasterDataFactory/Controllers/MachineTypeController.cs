using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.MachinesTypes;
using MasterDataFactory.Models.Domain.MachineTypes;
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
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.MachineTypes.Add(new MachineType("PCX010","maquina1"));
                _context.SaveChanges();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MachineTypeDTO>> GetMachineType(long id)
        {
            MachineTypeService machineTypeService = new MachineTypeService(_context);
            MachineTypeDTO machinetypeDTO = await machineTypeService.getMachineType(id); 
            if (machineTypeService == null)
            {
                return NotFound();
            }
            return machinetypeDTO;
        }

        [HttpPost]
        public async Task<ActionResult<MachineTypeDTO>> PostTodoItem([FromBody]MachineType item)       
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MachineTypeService machineTypeService = new MachineTypeService(_context);
            machineTypeService.postMachineType(item);
            return CreatedAtAction(nameof(GetMachineType), new { id = item.id }, item);
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
        public void Delete(int id)
        {
        }
        */
    }
}
