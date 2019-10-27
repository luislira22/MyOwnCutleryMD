using System;
using System.Collections.Generic;
using MasterDataFactory.Services;
using Xunit;

namespace TestProject.MasterDataFactory.Services
{
    public class MachineServicesTest
    {
        [Fact]
        public async void EnsureMachineIsFoundById()
        {
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedMachines(context);
            
            var machineService = new MachineService(context);
            
            var machineId = new Guid("11111111-1111-1111-1111-111111111111");
            var expectedMachineTypeId = new Guid("11111111-1111-1111-1111-111111111111"); //TODO quando o Tomas fizer os testes dele
            const string expectedMachineBrand = "Siemens";
            const string expectedMachineModel = "HO-501";
            const string expectedMachineLocation = "Sector 10";
            
            var result = await machineService.GetMachineById(machineId);
            
            Assert.True(expectedMachineTypeId.Equals(result.MachineType.Id));
            Assert.True(expectedMachineBrand.Equals(result.MachineBrand.Brand));
            Assert.True(expectedMachineModel.Equals(result.MachineModel.Model));
            Assert.True(expectedMachineLocation.Equals(result.MachineLocation.Location));
        }
        
        [Fact]
        public async void EnsureMachineIsNotFoundById()
        {
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedMachines(context);
            
            var machineService = new MachineService(context);
            
            var machineId = new Guid("11111111-2222-2222-2222-111111111111");
            
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>(machineService.GetMachineById(machineId)));
        }
        
        [Fact]
        public async void EnsureMachineIsFoundByMachineType()
        {
            //TODO Lira   
        }

        [Fact]
        public async void EnsureMachineTypeIsUpdated()
        {
            //TODO Tomas   
        }
    }
}