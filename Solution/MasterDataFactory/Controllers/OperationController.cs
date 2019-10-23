using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.PersistenceContext;
using Microsoft.EntityFrameworkCore;

namespace MasterDataFactory.Controllers
{
    [Route("operation")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly Context _context;
        private readonly OperationService _service;
        
        public OperationController(Context _context){
            this._context = _context;
            this._service = new OperationService(_context);
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDTO>> GetOperation(Guid id)
        {
            OperationDTO operationDto = await _service.getOperation(id); 
            if (operationDto == null)
                return NotFound();
            return operationDto;
        }

        [HttpPost]
        public async Task<ActionResult<OperationDTO>> PostOperation(Operation Operation)
        {
            if (_service.postOperation(Operation))
                return CreatedAtAction(nameof(GetOperation), new {id = Operation.Id}, Operation);
            else
                return BadRequest();
        }
}
}