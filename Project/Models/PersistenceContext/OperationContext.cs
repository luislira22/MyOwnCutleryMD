using Microsoft.EntityFrameworkCore;
using Project.Models.Domain.MasterDataFactory.Operations;

namespace Project.Models.PersistenceContext
{
    public class OperationContext : DbContext
    {
        public OperationContext(DbContextOptions<OperationContext> options) : base(options)
        {

        }

        public DbSet<Operation> Operations { get; set; }
    }
}