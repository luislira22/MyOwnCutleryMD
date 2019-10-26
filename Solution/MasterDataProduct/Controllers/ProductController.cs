using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataProduct.DTO;
using MasterDataProduct.Models.Products;
using MasterDataProduct.PersistenceContext;
using MasterDataProduct.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _serviceProduct;


        public ProductController(Context context)
        {
            _serviceProduct = new ProductService(context);

            if (!context.Products.Any())
            {
                context.Products.Add(new Product(new ManufacturingPlan("planoTeste1")));
                context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                return await _serviceProduct.GetProducts();
            }
            catch (NullReferenceException)
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(Guid id)
        {
            try
            {
                var product = await _serviceProduct.GetProduct(id);
                return Ok(product.ToDto());
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct([FromBody] ProductDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = await _serviceProduct.PostProduct(item);
                return CreatedAtAction("PostProduct", product.ToDto());
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // DELETE: api/machine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(Guid id)
        {
            try
            {
                await _serviceProduct.DeleteProduct(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Machine not found");
            }
        }

        [HttpGet("plan/{id}")]
        public async Task<ActionResult<ManufacturingPlanDTO>> GetProductionPlanFromProduct(Guid id)
        {
            //FIX Corregir posteriormente, não está grande coisa o construtor do DTO
            var product = await _serviceProduct.GetProduct(id);
            return new ManufacturingPlanDTO(product.Plan.Name);
        }
    }
}