using System;
using System.Collections.Generic;
using System.Linq;
using MasterDataFactory.DTO.ProductionLines;
using MasterDataFactory.Models.Machines;

namespace MasterDataFactory.Models.ProductionLines
{
    public class ProductionLine : IEntity
    {
        public Guid Id { get; set; }

        public virtual List<Machine> Machines { get; set; }
        public virtual ProductionLineDescription description {get;set;}
        protected ProductionLine(){
            
        }

        public ProductionLine(ProductionLineDescription description, List<Machine> Machines){
            this.description = description;
            this.Machines = Machines;
        }

        public ProductionLineDTO toDTO(){
            List<string> machines = Machines.Select(m => m.Id.ToString()).ToList();
            return new ProductionLineDTO(Id.ToString(), description.Description, machines);
        }

    }
}
