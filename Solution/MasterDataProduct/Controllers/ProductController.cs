using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MasterDataProduct.DTO;
using MasterDataProduct.DTO.Products;
using MasterDataProduct.Helpers;
using MasterDataProduct.Models.Products;
using MasterDataProduct.PersistenceContext;
using MasterDataProduct.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataProduct.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _serviceProduct;


        public ProductController(Context context)
        {
            _serviceProduct = new ProductService(context);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            try
            {
                var products = (List<Product>) (await _serviceProduct.GetProducts());
                return products.Select(m => m.ToDto()).ToList();
            }
            catch (NullReferenceException)
            {
                return NoContent();
            }
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(Guid id)
        {
            try
            {
                var product = await _serviceProduct.GetProduct(id);
                //return Ok(product);
                return Ok(product.ToDto());
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct([FromBody] ProductDTO dto)
        {
            try
            {
                //Create Manufacturing plan
                ManufacturingPlan manufacturingPlan = _serviceProduct.CreateManufacturingPlan(dto.Plan.Operations);
                //Create Product
                Product product = new Product(new Ref(dto.Ref), manufacturingPlan);
                //Create Product
                var productResponse = await _serviceProduct.PostProduct(product);

                return CreatedAtAction("PostProduct", productResponse.ToDto());
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (FormatException e)
            {
                return BadRequest(e.Message);
            }
            catch (WebException e)
            {
                Console.Write(e.StackTrace);
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/machine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _serviceProduct.DeleteProduct(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found");
            }
        }

        //TODO
        /*
        [HttpGet("plan/{id}")]
        public async Task<ActionResult<ManufacturingPlanDTO>> GetProductionPlanFromProduct(Guid id)
        {
            //FIX Corregir posteriormente, não está grande coisa o construtor do DTO
            var product = await _serviceProduct.GetProduct(id);
            return new ManufacturingPlanDTO(product.Plan.Name);
        }
        */
    }
}