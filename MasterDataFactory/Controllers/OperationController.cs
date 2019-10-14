using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.PersistenceContext;

namespace MasterDataFactory.Controllers
{
    [Route("operation")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly Context _context;

        
        public OperationController(Context _context){
            this._context = _context;

        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDTO>> GetOperation(long id)
        {
            OperationService operationService = new OperationService(_context);
            OperationDTO operationDTO = await operationService.getOperation(id); 
            if (operationService == null)
            {
                return NotFound();
            }
            return operationDTO;
        }

        [HttpPost]
        public async Task<ActionResult<OperationDTO>> PostOperation(Operation Operation)
        {
            OperationService operationService = new OperationService(_context);
            operationService.postOperation(Operation);
            return CreatedAtAction(nameof(GetOperation), new { id = Operation.Id }, Operation);
        }
}
}