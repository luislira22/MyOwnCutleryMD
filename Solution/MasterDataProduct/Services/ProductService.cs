using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataProduct.DTO;
using MasterDataProduct.Models.Domain.Products;
using MasterDataProduct.Models.PersistenceContext;
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
            return product;
        }

        public async Task PostProduct(Product product)
        {
            await _productRepository.Create(product);
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _productRepository.GetAll();
        }
    }
}