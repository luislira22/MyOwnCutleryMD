using System.Collections.Generic;

namespace MasterDataProduct.DTO.Products
{
    public class ManufacturingPlanDTO
    {
        public ICollection<string> Operations { get; set; }

        public ManufacturingPlanDTO(ICollection<string> operations)
        {
            Operations = operations;
        }
    }
}