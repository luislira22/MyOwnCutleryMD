namespace MasterDataProduct.Models.Domain.Products
{
    public class ProductDTO
    {
        public string Ref {get;set;}
        public string Plan {get;set;}

        public ProductDTO(string Ref, string Plan){
            this.Ref = Ref;
            this.Plan = Plan;
        }
    }
}