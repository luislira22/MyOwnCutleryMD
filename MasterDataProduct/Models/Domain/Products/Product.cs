namespace MasterDataProduct.Models.Domain.Products
{
    public class Product
    {
        public ProductId Id { get; set; }

        public string Name { get; set; }

        //Confirmar se é mesmo assim quando saírem os casos de uso    
        public ManufacturingPlan Plan { get; set; }
    }
}