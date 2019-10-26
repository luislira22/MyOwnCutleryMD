using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataProduct.Models.Products
{
    public class ProductId
    {
        public string Id { get; set; }

        public ProductId(string id)
        {
            this.Id = id;
        }
    }


    public class ProductIdValueVConverter : ValueConverter<ProductId, string>
    {
        public ProductIdValueVConverter(ConverterMappingHints mappingHints = null)
            : base(
                id => id.Id,
                reference => new ProductId(reference),
                mappingHints
            )
        {
        }
    }
}

