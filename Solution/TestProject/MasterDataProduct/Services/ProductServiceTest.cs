using System;
using System.Collections.Generic;
using System.Linq;
using MasterDataProduct.Models.Products;
using MasterDataProduct.Services;
using TestProject.MasterDataProduct.ContextMock;
using Xunit;

namespace TestProject.MasterDataProduct.Services
{
    public class ProductServiceTest
    {
        [Fact]
        public async void Ensure_get_product_by_id_is_found()
        {
            Guid operationId = new Guid("12345678-1234-1234-1234-123412341234");
            //mock
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedProducts(context);
            var service = new ProductService(context);
            var result = await service.GetProduct(operationId);
            const string expectedPlanName = "UnitTestPlan";
            Assert.True(expectedPlanName.Equals(result.Plan.Name));
        }

        [Fact]
        public void Ensure_get_product_by_id_not_found_throws_exception()
        {
            Guid operationId = new Guid("22345678-1234-1234-1234-123412341234");
            //mock
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedProducts(context);
            var service = new ProductService(context);
            
            Assert.ThrowsAsync<KeyNotFoundException>(() => (service.GetProduct(operationId)));
        }

        [Fact]
        public void Ensure_product_to_delete_not_found_throws_exception()
        {
            Guid operationId = new Guid("22345678-1234-1234-1234-123412341234");
            //mock 
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedProducts(context);

            ProductService service = new ProductService(context);
            Assert.ThrowsAsync<KeyNotFoundException>(() => (service.DeleteProduct(operationId)));
        }
        
    }
}