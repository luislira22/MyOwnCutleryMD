namespace MasterDataProduct.Models.Domain.Products
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public string Plan { get; set; }

        public ProductDTO(string id, string name, string plan)
        {
            Id = id;
            Name = name;
            Plan = plan;
        }
    }
}