using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using MasterDataProduct.Models.PersistenceContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterDataProduct.Models.Domain.Products
{
    public class ProductService
    {
        private readonly Context _context;

        public ProductService(Context context)
        {
            _context = context;
        }

        public async Task<ProductDTO> GetProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            return product.toDTO();
        }

        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
    }
}