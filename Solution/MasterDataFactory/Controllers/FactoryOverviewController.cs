using System.Threading.Tasks;
using MasterDataFactory.DTO.ProductionPlanning;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryOverviewController : ControllerBase
    {

        private readonly FactoryOverviewService _factoryOverviewService;

        public FactoryOverviewController(Context context)
        {
            _factoryOverviewService = new FactoryOverviewService(context);
        }
        
        [HttpGet]
        public async Task<ActionResult<FactoryOverviewDTO>> GetFactoryOverview()
        {
            return await _factoryOverviewService.GetFactoryOverview();
        }

    }
}