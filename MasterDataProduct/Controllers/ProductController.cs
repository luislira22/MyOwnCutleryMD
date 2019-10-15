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
        private readonly ProductService _service;

        public ProductController(Context context)
        {
            _service = new ProductService(context);
            //context.AddAsync(new Product());
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            await _service.CreateProduct(product);
            /*_context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            */
            return CreatedAtAction("CreateProduct", null);
        }
        
    }
}