using Microsoft.EntityFrameworkCore;
using MasterDataFactory.Models.Domain.Operations;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class OperationContext : DbContext
    {
        public OperationContext(DbContextOptions<OperationContext> options) : base(options)
        {

        }

        public DbSet<Operation> Operations { get; set; }
    }
}