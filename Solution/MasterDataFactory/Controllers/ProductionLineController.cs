using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.DTO.ProductionLines;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Models.ProductionLines;
using MasterDataFactory.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineController : ControllerBase
    {
        private readonly ProductionLineService _serviceProductionLine;

        public ProductionLineController(Context context){
            _serviceProductionLine = new ProductionLineService(context);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionLineDTO>> GetProductionLineById(Guid id)
        {
            try
            {
                ProductionLine productionLine = await _serviceProductionLine.GetProductionLineById(id);
                return productionLine.toDTO();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductionLineDTO>> PostProductionLine([FromBody]ProductionLineDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductionLine machine = await _serviceProductionLine.PostProductionLine(item);
                return CreatedAtAction(nameof(PostProductionLine), machine.toDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
    }
}