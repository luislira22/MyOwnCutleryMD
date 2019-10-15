using System.Threading.Tasks;
using MasterDataProduct.Models.PersistenceContext;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataProduct.Models.Domain.Products
{
    public class ProductService
    {
        private readonly Context _context;

        public ProductService(Context context)
        {
            _context = context;
        }

        public async Task<ProductDTO> GetProductById(long id)
        {
            var product = await _context.Products.FindAsync(id);
            return product.toDTO();
        }

        public async void CreateProduct(Product product, ManufacturingPlan plan)
        {
            product.Plan = plan;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}