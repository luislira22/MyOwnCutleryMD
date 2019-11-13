using System;

namespace MasterDataProduct.DTO.Products
{
    public class ProductDTO
    {
        public Guid Id;
        public string Ref { get; set; }
        public ManufacturingPlanDTO Plan { get; set; }

        public ProductDTO()
        {
            
        }
        
        public ProductDTO(Guid id,string refer, ManufacturingPlanDTO plan)
        {
            Id = id;
            Ref = refer;
            Plan = plan;
        }
    }
}