using Microsoft.EntityFrameworkCore;
using MasterDataFactory.Models.Domain.ProductionLines;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class ProductionLineContext : DbContext
    {
        public ProductionLineContext(DbContextOptions<ProductionLineContext> options) : base(options)
        {

        }

        public DbSet<ProductionLine> ProductionLines { get; set; }
    }
}