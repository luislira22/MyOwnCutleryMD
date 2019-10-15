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
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(long id)
        {
            var productService = new ProductService(_context);
            ProductDTO productDto = await productService.GetProductById(id);
            if (productService == null)
            {
                return NotFound();
            }

            return productDto;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] Product product,
            [FromBody] ManufacturingPlan plan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productService = new ProductService(_context);
            productService.CreateProduct(product, plan);
            return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
        }
    }
}