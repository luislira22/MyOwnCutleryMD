namespace MasterDataProduct.Models.Domain.Products
{

    public class Product
    {
        private ProductId id { get; set; }

        //Confirmar se é mesmo assim quando saírem os casos de uso    
        private ManufacturingPlan plan { get; set; }
    }
}

