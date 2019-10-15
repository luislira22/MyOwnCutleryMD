using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataProduct.Models.Domain.Products
{
    public class ProductId
    {
        public long Id { get; set; }

        public ProductId(long id)
        {
            Id = id;
        }
    }
    
    public class ProductIdValueVConverter : ValueConverter<ProductId, long>
    {
        public ProductIdValueVConverter(ConverterMappingHints mappingHints = null)
            : base(
                id => id.Id,
                id => new ProductId(id),
                mappingHints
            )
        {
        }
    }
}