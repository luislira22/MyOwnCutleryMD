using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using MasterDataProduct.DTO;
using MasterDataProduct.DTO.Products;
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

        public async Task<Product> PostProduct(Product product)
        {
            await _productRepository.Create(product);
            return product;
        }


        public async Task DeleteProduct(Guid id)
        {
            var p = await _productRepository.GetById(id);
            if (p == null) throw new KeyNotFoundException();

            await _productRepository.Delete(id);
        }
        
        public ManufacturingPlan CreateManufacturingPlan(ICollection<string> idsCollection)
        {
            ManufacturingPlan manufacturingPlan = new ManufacturingPlan();
            foreach (var guidString in idsCollection)
            {
                Guid guid;
                if (!Guid.TryParse(guidString, out guid))
                    throw new FormatException("Invalid Guid format.");

                WebRequest request = WebRequest.Create("https://localhost:5001/api/operation/exists/" + guidString);
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();

                if (response.StatusDescription != "OK")
                    throw new WebException("Can't find api request URL");

                bool exists;
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    exists = Boolean.Parse(reader.ReadToEnd());
                }

                response.Close();

                if(!manufacturingPlan.Ids.Contains(new OperationId(guidString))) 
                    manufacturingPlan.AddOperationId(guidString);
            }

            return manufacturingPlan;
        }
    }
}