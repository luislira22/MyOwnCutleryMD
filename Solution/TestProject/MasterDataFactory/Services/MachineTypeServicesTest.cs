using System;
using Xunit;
using MasterDataFactory.Services;
using System.Collections.Generic;

namespace TestProject.MasterDataFactory.Services
{
    public class MachineTypeServicesTest
    {
        [Fact]
        public async void EnsureMachineTypeIsFoundById()
        {
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedMachineTypes(context);
            
            var MachineTypeService = new MachineTypeService(context);
            
            var MachineTypeId = new Guid("21111111-1111-1111-1111-111111111111");
            var OperationId = new Guid("12345678-1234-1234-1234-123412341234");
            const string expectedMachineTypeDescription = "Trituradora";
            
            var result = await MachineTypeService.getMachineType(MachineTypeId);
            
            Assert.Equal(expectedMachineTypeDescription,result.Type.Type);
            Assert.Equal(result.MachineTypeOperations[0].OperationId, OperationId);
            Assert.Equal(result.MachineTypeOperations[0].MachineTypeId, MachineTypeId);
        }
        
        [Fact]
        public async void EnsureMachineTypeIsNotFoundById()
        {
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedMachineTypes(context);
            
            var MachineTypeService = new MachineTypeService(context);
            
            var MachineTypeId = new Guid("51111151-1111-1111-1111-111111111111");
            
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>(MachineTypeService.getMachineType(MachineTypeId)));
        }
    }
}