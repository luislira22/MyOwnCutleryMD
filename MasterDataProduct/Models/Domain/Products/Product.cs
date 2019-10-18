using System;

namespace MasterDataProduct.Models.Domain.Products
{
    public class Product : IEntity
    {
        
        public Guid Id { get; set; }
        public ManufacturingPlan Plan { get; set; }

        public Product()
        {

        }

        public Product( ManufacturingPlan Plan)
        {
            this.Plan = Plan;
        }

        public ProductDTO toDTO()
        {
            return new ProductDTO(Id.ToString(), Plan.Name);
        }

       
    }
}