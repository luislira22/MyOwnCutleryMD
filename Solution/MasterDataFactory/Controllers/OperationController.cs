using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Services;


namespace MasterDataFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly OperationService _service;
        
        public OperationController(Context _context){
            this._service = new OperationService(_context);
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDTO>> GetOperation(Guid id)
        {
            Operation operation = await _service.getOperation(id); 
            if (operation == null)
                return NotFound();
            return new OperationDTO(operation.Id,operation.Description.Description);
        }

        [HttpPost]
        public async Task<ActionResult<OperationDTO>> PostOperation(OperationDTO operationDto)
        {
            //create operation from DTO
            Operation operation = new Operation(operationDto.Description);
            try
            {
                _service.postOperation(operation);
                return CreatedAtAction(nameof(GetOperation), new {id = operation.Id}, operation.toDTO());
            }
            catch (Exception e)
            {
                return BadRequest("operation failed.");
            }
        }
        
        
        
}
}