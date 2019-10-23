namespace MasterDataProduct.DTO
{
    public class ProductDTO
    {
        public string Ref { get; set; }
        public string Plan { get; set; }

        public ProductDTO(string refer, string plan)
        {
            Ref = refer;
            Plan = plan;
        }
    }
}