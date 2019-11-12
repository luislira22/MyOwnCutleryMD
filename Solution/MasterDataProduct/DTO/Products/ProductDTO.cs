namespace MasterDataProduct.DTO.Products
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Ref { get; set; }
        public ManufacturingPlanDTO Plan { get; set; }

        public ProductDTO(string id, string refer, ManufacturingPlanDTO plan)
        {
            Id = id;
            Ref = refer;
            Plan = plan;
        }
    }
}