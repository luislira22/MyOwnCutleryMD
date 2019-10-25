using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MasterDataFactory.DTO.Operations;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Services;

namespace MasterDataFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly OperationService _service;

        public OperationController(Context _context)
        {
            _service = new OperationService(_context);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDTO>> GetOperationById(Guid id)
        {
            try
            {
                Operation operation = await _service.getOperationById(id);
                return operation.toDTO();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OperationDTO>> PostOperation(OperationDTO operationDto)
        {
            Operation operation = new Operation(operationDto);
            try
            {
                //try to post object
                await _service.postOperation(operation);
                return CreatedAtAction("PostOperation", operation.toDTO());
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return BadRequest("operation failed.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOperation(Guid id)
        {
            try
            {
                await _service.deleteOperation(id);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}