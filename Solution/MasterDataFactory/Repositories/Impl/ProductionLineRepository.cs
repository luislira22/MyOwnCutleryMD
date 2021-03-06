using System;
using System.Collections.Generic;
using System.Linq;
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
            //context.ProductionLines.Include(p => p.Machines).ToListAsync();
        }

        public override async Task<bool> Exists(Guid id)
        {
            return await _context.ProductionLines.AnyAsync(o => o.Id == id);
        }
        
        public async Task<List<ProductionLine>> GetProductionLineByMachine(Guid machineId)
        {
            //return await _context.ProductionLines.FirstAsync();
            //return await _context.ProductionLines.FirstAsync(o => o.Machines.Any(m => m.Id == machineId));
            return await _context.ProductionLines.Where(o => o.Machines.Any(m => m.Id == machineId)).ToListAsync();
        }
    }
}