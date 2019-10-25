namespace MasterDataProduct.DTO
{
    public class ManufacturingPlanDTO
    {
        public string Name { get; set; }

        public ManufacturingPlanDTO(string name)
        {
            Name = name;
        }
    }
}