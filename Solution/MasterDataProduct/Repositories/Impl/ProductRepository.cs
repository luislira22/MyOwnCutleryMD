using System;
using System.Threading.Tasks;
using MasterDataProduct.Models.Domain.Products;
using MasterDataProduct.Models.PersistenceContext;
using MasterDataProduct.Repositories.Interfaces;

namespace MasterDataProduct.Repositories.Impl
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(Context context) : base(context)
        {
        }

        public override Task<bool> Exists(Guid id)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}