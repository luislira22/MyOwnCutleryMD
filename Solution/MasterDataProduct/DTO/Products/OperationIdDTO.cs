using System.Collections.Generic;
using MasterDataProduct.Models.Products;

namespace MasterDataProduct.DTO.Products
{
    public class OperationIdDTO
    {
        public string Id { get; set; }

        public OperationIdDTO(string Id)
        {
            this.Id = Id;
        }
    }
}