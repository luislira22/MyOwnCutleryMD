using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MasterDataFactory.DTO.Operations;
using MasterDataFactory.Helpers;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Services;
using Microsoft.AspNetCore.Authorization;

namespace MasterDataFactory.Controllers
{    
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly OperationService _service;

        public OperationController(Context _context)
        {
            _service = new OperationService(_context);
        }
        
        [AllowAnonymous]
        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> OperationExists(Guid id)
        {
            if (await _service.OperationExists(id))
                return Ok(true);
            return Ok(false);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationDTO>>> GetOperations()
        {
            IEnumerable<Operation> operations = (await _service.GetOperations());
            return operations.Select(m => m.ToDTO()).ToList();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDTO>> GetOperationById(Guid id)
        {
            try
            {
                Operation operation = await _service.GetOperationById(id);
                return operation.ToDTO();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OperationDTO>> PostOperation(OperationDTO operationDto)
        {
            try
            {
                Operation operation = new Operation(operationDto);
                
                //try to post object
                await _service.PostOperation(operation);
                return CreatedAtAction("PostOperation", operation.ToDTO());
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOperation(Guid id)
        {
            try
            {
                await _service.DeleteOperationById(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
    }
}