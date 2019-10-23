using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataProduct.DTO;
using MasterDataProduct.Models.Domain.Products;
using MasterDataProduct.Models.PersistenceContext;
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
            return await _serviceProduct.GetProducts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product= await _serviceProduct.GetProduct(id);
            return product.ToDto();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct([FromBody] Product item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _serviceProduct.PostProduct(item);
            return CreatedAtAction(nameof(GetProduct), new {id = item.Id}, item);
        }
    }
}