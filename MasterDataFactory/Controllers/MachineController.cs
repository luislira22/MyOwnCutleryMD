using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Machines;
using MasterDataFactory.Models.PersistenceContext;
using System;

namespace MasterDataFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly MachineService _service;

        public MachineController(MachineContext context)
        {
            _service = new MachineService(context);
            //context.AddAsync(new Machine());
        }

        // GET: api/machine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Machine>>> GetTodoItems()
        {
            return await _service.GetMachines();
        }

        // POST: api/machine
        [HttpPost]
        public async Task<ActionResult<Machine>> CreateMachine(Machine machine)
        {
            await _service.CreateMachine(machine);
            /*_context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            */
            //return CreatedAtAction(nameof(machine), new { id = machine.Id }, machine); <-- supostamente e um bug
            return CreatedAtAction("CreateMachine", null);
        }
    }
}