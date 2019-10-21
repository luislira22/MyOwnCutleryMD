using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataProduct.Models.Domain.Products;
using MasterDataProduct.Models.PersistenceContext;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;

            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product(new ManufacturingPlan("planoTeste1")));
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await new ProductService(_context).GetProducts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var productService = new ProductService(_context);
            var productDto = await productService.GetProduct(id);
            return productDto;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct([FromBody] Product item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productService = new ProductService(_context);
            await productService.PostProduct(item);
            return CreatedAtAction(nameof(GetProduct), new {id = item.Id}, item);
        }
    }
}