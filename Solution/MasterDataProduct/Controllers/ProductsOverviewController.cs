using System.Threading.Tasks;
using MasterDataProduct.DTO.ProductionPlanning;
using MasterDataProduct.PersistenceContext;
using MasterDataProduct.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataProduct.Controllers
{    
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsOverviewController : ControllerBase
    {
        
        private readonly ProductService _productService;

        public ProductsOverviewController(Context context)
        {
            _productService = new ProductService(context);
        }
        [HttpGet]
        public async Task<ActionResult<ProductsOverviewDTO>> GetProductsOverview()
        {
            return await _productService.GetProductsOverview();
        }
    }
}