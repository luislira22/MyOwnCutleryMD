using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.DTO.ProductionLines;
using MasterDataFactory.Helpers;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Models.ProductionLines;
using MasterDataFactory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataFactory.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionLineController : ControllerBase
    {
        private readonly ProductionLineService _serviceProductionLine;

        public ProductionLineController(Context context)
        {
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionLineDTO>>> GetProductionLines()
        {
            IEnumerable<ProductionLine> productionLines = (await _serviceProductionLine.GetProductionLines());
            return productionLines.Select(l => l.toDTO()).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<ProductionLineDTO>> PostProductionLine([FromBody] ProductionLineDTO item)
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductionLine(Guid id)
        {
            try
            {
                await _serviceProductionLine.DeleteProductionLineById(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductionLineDTO>> UpdateProductionLine(Guid id,[FromBody] ProductionLineDTO item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                await _serviceProductionLine.UpdateProductionLine(id, item);
                ProductionLine productionLine = await _serviceProductionLine.GetProductionLineById(id);
                return productionLine.toDTO();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}