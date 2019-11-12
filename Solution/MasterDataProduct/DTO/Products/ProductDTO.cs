namespace MasterDataProduct.DTO.Products
{
    public class ProductDTO
    {
        public string Ref { get; set; }
        public ManufacturingPlanDTO Plan { get; set; }

        public ProductDTO(string refer, ManufacturingPlanDTO plan)
        {
            Ref = refer;
            Plan = plan;
        }
    }
}