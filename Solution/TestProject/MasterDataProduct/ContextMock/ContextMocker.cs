using System;
using MasterDataProduct.Models.Products;
using MasterDataProduct.PersistenceContext;
using Microsoft.EntityFrameworkCore;

namespace TestProject.MasterDataProduct.ContextMock
{
    public class ContextMocker
    {
        public static Context GetContextMock()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("TestDB")
                .Options;
            var dbContext = new Context(options);
            return dbContext;
        }

        public static void SeedProducts(Context dbContext)
        {
            var product1 = new Product(new Guid("12345678-1234-1234-1234-123412341234"),
                new ManufacturingPlan("UnitTestPlan"));
            dbContext.Products.Add(product1);
            
            var product2 = new Product(new Guid("12345678-1234-1234-1234-123412341235"),
                new ManufacturingPlan("UnitTestPlan"));
            dbContext.Products.Add(product1);
            
            
        }
    }
}