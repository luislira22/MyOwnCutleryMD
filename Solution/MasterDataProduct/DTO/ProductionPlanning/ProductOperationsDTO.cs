using System.Collections;
using System.Collections.Generic;

namespace MasterDataProduct.DTO.ProductionPlanning
{
    public class ProductOperationsDTO
    {
        public string Product { get; set; }
        public IEnumerable<string> Operations { get; set;}
    }
}