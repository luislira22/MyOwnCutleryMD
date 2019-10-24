using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Services;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


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
            if (await _service.existsOperation(id))
            {
                return NotFound();
            }
            Operation operation = await _service.getOperationById(id);
            return operation.toDTO();
        }

        [HttpPost]
        public async Task<ActionResult<OperationDTO>> PostOperation(OperationDTO operationDto)
        {
            //create operation from DTO
            Operation operation = new Operation(operationDto);
            try
            {
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
            if (await _service.existsOperation(id))
            {
                return NotFound();
            }
            await _service.deleteOperation(id);
            return Ok();
        }
    }
}