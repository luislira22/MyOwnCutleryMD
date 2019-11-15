using System;
using MasterDataProduct.Models.Products;
using Xunit;

namespace TestProject.MasterDataProduct.Domain
{
    public class ProductTest
    {
        [Fact]
        public void Ensure_product_contains_id()
        {
            Product p = new Product(new Ref("produto1"), new ManufacturingPlan());
            Assert.True(p.Id != null);
        }

        [Fact]
        public void Ensure_product_contains_a_plan()
        {
            Product p = new Product(new Ref("produto1"), new ManufacturingPlan());
            Assert.True(p.Plan != null);
        }

        [Fact]
        public void Ensure_plan_is_not_null()
        {
            Product p = new Product(new Ref("produto1"), new ManufacturingPlan());
            p.Plan.AddOperationId("12345678-1234-1234-1234-123412341234");
            p.Plan.AddOperationId("12345678-1234-1234-1234-123412341235");
            p.Plan.AddOperationId("12345678-1234-1234-1234-123412341236");
            var Three = 3;
            Assert.Equal(Three,p.Plan.Ids.Count);
        }
    }
}