using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MasterDataProduct.DTO.Products;

namespace MasterDataProduct.Models.Products
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        
        public Ref Reference { get; set; }
        public ManufacturingPlan Plan { get; set; }

        public Product()
        {
            
        }

        public Product(Ref reference,ManufacturingPlan plan)
        {
            Reference = reference;
            Plan = plan;
        }

        public Product(ProductDTO dto)
        {
            Plan = new ManufacturingPlan();
            Reference = new Ref(dto.Ref);
        }

        public Product(Guid plan,Ref reference, ManufacturingPlan manufacturingPlan)
        {
            Id = plan;
            Reference = reference;
            Plan = manufacturingPlan;
        }

        public ProductDTO ToDto()
        {
            ICollection<OperationIdDTO> collection = new Collection<OperationIdDTO>();
            foreach (OperationId opId in Plan.Ids)
            {
                collection.Add(opId.toDTO());
            }
            return new ProductDTO(Id,Reference.Value,new ManufacturingPlanDTO(collection));
        }
    }
}