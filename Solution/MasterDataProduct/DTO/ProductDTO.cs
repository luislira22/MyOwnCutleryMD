namespace MasterDataProduct.DTO
{
    public class ProductDto
    {
        public string Ref { get; set; }
        public string Plan { get; set; }

        public ProductDto(string refer, string plan)
        {
            Ref = refer;
            Plan = plan;
        }
    }
}