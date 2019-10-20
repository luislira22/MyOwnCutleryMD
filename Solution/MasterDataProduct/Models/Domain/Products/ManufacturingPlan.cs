using System;

namespace MasterDataProduct.Models.Domain.Products
{
    public class ManufacturingPlan
    {
        public string Name { get; set; }

        public ManufacturingPlan(string name)
        {
            Name = name;
        }
    }
}