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


    public class ProductIdValueVConverter : ValueConverter<ProductId, string>
    {
        public ProductIdValueVConverter(ConverterMappingHints mappingHints = null)
            : base(
                id => id.id,
                reference => new ProductId(reference),
                mappingHints
            )
        {
        }
    }
}

