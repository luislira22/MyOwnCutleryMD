using System;
using Xunit;
using MasterDataFactory.Services;
using System.Collections.Generic;
using MasterDataFactory.Models.ProductionLines;

namespace TestProject.MasterDataFactory.Services
{
    public class ProductionLineServicesTest
    {
        [Fact]
        public async void EnsureProductionLineIsFoundById()
        {
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedProductionLines(context);

            var ProductionLineService = new ProductionLineService(context);


            var ProductionLineId = new Guid("12111111-1111-1111-1111-111111111111");
            var expectedProductionLineId = new Guid("12111111-1111-1111-1111-111111111111");
            var expectedMachineId = new Guid("11111111-1111-1111-1111-111111111111");
            var expectedProductionLineDescription = new ProductionLineDescription("Linha1");
            var result = await ProductionLineService.GetProductionLineById(ProductionLineId);
            
            Assert.Equal(expectedProductionLineId, result.Id);
            Assert.Equal(expectedProductionLineDescription, result.Description);
            Assert.Equal(expectedMachineId, result.Machines[0].Id);
        }

        [Fact]
        public async void EnsureProductionLineIsNotFoundById()
        {
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedProductionLines(context);

            var ProductionLineService = new ProductionLineService(context);

            var ProductionLineId = new Guid("11113111-2222-2222-2222-111111111111");

            await Assert.ThrowsAsync<KeyNotFoundException>(() => (ProductionLineService.GetProductionLineById(ProductionLineId)));
        }
    }
}