using MasterDataProduct.Models.Domain.Products;
using Xunit;

namespace TestProject.MasterDataProduct.Domain
{
    public class ProductTest
    {
        [Fact]
        public void Ensure_product_contains_id()
        {
            Product p = new Product(new ManufacturingPlan("PlanoTeste"));
            Assert.True(p.Id != null);
        }
        
        [Fact]
        public void Ensure_product_contains_a_plan()
        {
            Product p = new Product(new ManufacturingPlan("PlanoTeste"));
            Assert.True(p.Plan!=null);
        }
    }
}