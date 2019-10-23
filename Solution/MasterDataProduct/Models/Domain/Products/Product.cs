using System;
using MasterDataProduct.DTO;

namespace MasterDataProduct.Models.Domain.Products
{
    public class Product : IEntity
    {
        
        public Guid Id { get; set; }
        public ManufacturingPlan Plan { get; set; }

        public Product()
        {

        }

        public Product( ManufacturingPlan plan)
        {
            this.Plan = plan;
        }

        public ProductDto ToDto()
        {
            return new ProductDto(Id.ToString(), Plan.Name);
        }

       
    }
}