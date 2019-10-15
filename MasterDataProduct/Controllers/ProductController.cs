using MasterDataProduct.Models.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;
        
    }
}