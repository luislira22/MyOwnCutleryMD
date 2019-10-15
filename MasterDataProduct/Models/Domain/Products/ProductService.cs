using MasterDataProduct.Models.PersistenceContext;

namespace MasterDataProduct.Models.Domain.Products
{
    public class ProductService
    {
        private readonly Context _context;

        public ProductService(Context context)
        {
            _context = context;
        }
        
        
    }
}