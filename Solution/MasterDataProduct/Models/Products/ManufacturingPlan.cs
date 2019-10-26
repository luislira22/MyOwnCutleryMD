namespace MasterDataProduct.Models.Products
{
    public class ManufacturingPlan
    {
        public string Name { get; set; }

        public ManufacturingPlan(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name = name;
        }

       
    }
}