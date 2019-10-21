using System;
using MasterDataProduct.Models.Domain.Products;
using Xunit;


namespace Tests
{
    public class ProductControllerTest
    {
        [Fact]
        public void EnsureProductIsCreated()
        {
            var p = new Product();
            Console.WriteLine("ExampleTest");
        }
    }
}