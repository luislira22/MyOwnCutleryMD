using Microsoft.EntityFrameworkCore;
using MasterDataFactory.Models.Domain.MachinesTypes;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class MachineTypeContext : DbContext
    {
        public MachineTypeContext(DbContextOptions<MachineTypeContext> options) : base(options)
        {

        }

        public DbSet<MachineType> MachineTypes { get; set; }
    }
}