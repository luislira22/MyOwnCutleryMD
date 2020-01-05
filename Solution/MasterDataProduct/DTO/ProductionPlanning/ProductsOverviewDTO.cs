using System.Collections;
using System.Collections.Generic;

namespace MasterDataProduct.DTO.ProductionPlanning
{
    public class ProductsOverviewDTO
    {
        public IEnumerable<ProductOperationsDTO> ProductOperations { get; set; }
    }
}