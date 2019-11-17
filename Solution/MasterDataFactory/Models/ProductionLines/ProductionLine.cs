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
        protected ProductionLine(){
            
        }

        public ProductionLine(List<Machine> Machines){
            this.Machines = Machines;
        }

        public ProductionLineDTO toDTO(){
            List<string> machines = Machines.Select(m => m.Id.ToString()).ToList();
            return new ProductionLineDTO(Id.ToString(),machines);
        }

    }
}
