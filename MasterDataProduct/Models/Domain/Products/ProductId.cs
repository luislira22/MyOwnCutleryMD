using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataProduct.Models.Domain.Products
{
    public class ProductId
    {
        public string id { get; set; }

        public ProductId(string id)
        {
            this.id = id;
        }
    }


    public class MachineTypeIdValueVConverter : ValueConverter<ProductId, string>
    {
        public MachineTypeIdValueVConverter(ConverterMappingHints mappingHints = null)
            : base(
                id => id.id,
                reference => new ProductId(reference),
                mappingHints
            )
        {
        }
    }
}

