using Microsoft.EntityFrameworkCore;
using MasterDataProduct.Models.Domain.Products;

namespace MasterDataProduct.Models.Domain.PersistenceContext
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}