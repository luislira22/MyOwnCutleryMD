using System;
using MasterDataProduct.DTO;

namespace MasterDataProduct.Models.Products
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
            Plan = plan;
        }

        public ProductDTO ToDto()
        {
            return new ProductDTO(Id.ToString(), new ManufacturingPlanDTO(Plan.Name));
        }

       
    }
}