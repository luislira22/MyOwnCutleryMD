using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataProduct.DTO;
using MasterDataProduct.Models.Products;
using MasterDataProduct.PersistenceContext;
using MasterDataProduct.Repositories.Impl;
using MasterDataProduct.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterDataProduct.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(Context context)
        {
            _productRepository = new ProductRepository(context);
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null) throw new KeyNotFoundException();
            return product;
        }
        

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _productRepository.GetAll();
        }
        
        public async Task<Product> PostProduct(ProductDTO productDTO)
        {
            var plan = new ManufacturingPlan(productDTO.Plan.Name);

            var product = new Product(plan);
            await _productRepository.Create(product);
            return product;
        }
    }
}