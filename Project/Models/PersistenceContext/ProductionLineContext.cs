using Microsoft.EntityFrameworkCore;
using Project.Models.Domain.MasterDataFactory.ProductionLines;

namespace Project.Models.PersistenceContext
{
    public class ProductionLineContext : DbContext
    {
        public ProductionLineContext(DbContextOptions<ProductionLineContext> options) : base(options)
        {

        }

        public DbSet<ProductionLine> ProductionLines { get; set; }
    }
}