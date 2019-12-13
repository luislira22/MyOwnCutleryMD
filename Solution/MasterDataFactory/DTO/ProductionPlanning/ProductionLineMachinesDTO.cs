using System.Collections;
using System.Collections.Generic;

namespace MasterDataFactory.DTO.ProductionPlanning
{
    public class ProductionLineMachinesDTO
    {
        public string ProductionLine;
        public IEnumerable<string> Machines;
        public ProductionLineMachinesDTO()
        {
            Machines = new List<string>();
        }
    }
}