using System;
using Xunit;
using MasterDataFactory.Services;
using System.Collections.Generic;

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
            var expectedProductionLineTypeId = new Guid("11111111-1111-1111-1111-111111111111"); //TODO quando o Tomas fizer os testes dele
            var expectedMachineId = new Guid("11111111-1111-1111-1111-111111111111");
            var result = await ProductionLineService.GetProductionLineById(ProductionLineId);
            

            Assert.True(expectedProductionLineTypeId.Equals(result.Id.Equals(ProductionLineId)));
            Assert.True(expectedProductionLineTypeId.Equals(result.Machines[0].Id.Equals(expectedMachineId)));
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