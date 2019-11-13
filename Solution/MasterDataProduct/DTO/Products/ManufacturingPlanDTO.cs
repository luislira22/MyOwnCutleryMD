using System.Collections.Generic;

namespace MasterDataProduct.DTO.Products
{
    public class ManufacturingPlanDTO
    {
        public ICollection<OperationIdDTO> Operations { get; set; }

        public ManufacturingPlanDTO(ICollection<OperationIdDTO> operations)
        {
            Operations = operations;
        }
    }
}