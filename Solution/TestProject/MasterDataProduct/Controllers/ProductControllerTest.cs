using System;
using System.Collections.Generic;
using MasterDataProduct.Controllers;
using TestProject.MasterDataProduct.ContextMock;
using Xunit;

namespace TestProject.MasterDataProduct.Controllers
{
    public class ProductControllerTest
    {
        [Fact]
        public void Ensure_get_product_by_id_not_found_throws_exception()
        {
            Guid operationId = new Guid("22345678-1234-1234-1234-123412341234");
            //mock
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedProducts(context);
            var controller = new ProductController(context);
            Assert.ThrowsAsync<KeyNotFoundException>(() => (controller.GetProduct(operationId)));
        }

        /*[Fact]
        public async void Ensure_controller_delete_by_id_fails_with_no_id()
        {
            Guid operationId = new Guid("12345678-1234-1234-1234-123412341247");
            //mock
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedProducts(context);
            var controller = new ProductController(context);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => (controller.DeleteProduct(operationId)));
        }*/
    }
}