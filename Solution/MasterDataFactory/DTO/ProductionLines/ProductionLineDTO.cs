using System;
using System.Collections.Generic;

namespace MasterDataFactory.DTO.ProductionLines
{
    public class ProductionLineDTO
    {
        public string Id;

        public string description;
        public List<string> Machines;

        public ProductionLineDTO(string id, string description, List<string> Machines){
            this.Id = id;
            this.description = description;
            this.Machines = Machines;
        }
        
    }
}