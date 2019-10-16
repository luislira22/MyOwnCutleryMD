namespace MasterDataProduct.Models.Domain.Products
{
    public class Product
    {
        public long id { get; set; } //id da base de dados

        public ProductId Ref { get; set; } //identificacao no dominio (value object) ex: PCX010
        public ManufacturingPlan Plan { get; set; }

        public Product()
        {

        }

        public Product(string Ref, ManufacturingPlan Plan)
        {
            this.Ref = new ProductId(Ref);
            this.Plan = Plan;
        }

        public ProductDTO toDTO()
        {
            return new ProductDTO(Ref.id, Plan.Name);
        }
    }
}