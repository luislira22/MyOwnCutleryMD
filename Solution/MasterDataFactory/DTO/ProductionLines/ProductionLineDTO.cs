using System;
using System.Collections.Generic;

namespace MasterDataFactory.DTO.ProductionLines
{
    public class ProductionLineDTO
    {
        public string Id;
        public List<string> Machines;

        public ProductionLineDTO(string id, List<string> Machines){
            this.Id = id;
            this.Machines = Machines;
        }
        
    }
}