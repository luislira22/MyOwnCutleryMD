using System;
using System.Threading.Tasks;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Models.ProductionLines;
using MasterDataFactory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MasterDataFactory.Repositories.Impl
{
    public class ProductionLineRepository : GenericRepository<ProductionLine>, IProductionLineRepository
    {
        public ProductionLineRepository(Context context) : base(context)
        {
            context.ProductionLines.Include(p => p.Machines).ToListAsync();
        }

        public override async Task<bool> Exists(Guid id)
        {
            return await _context.ProductionLines.AnyAsync(o => o.Id == id);
        }
    }
}