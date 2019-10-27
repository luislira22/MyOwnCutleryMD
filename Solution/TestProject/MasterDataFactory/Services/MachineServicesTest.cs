using System;
using System.Collections.Generic;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Services;
using MasterDataFactory.Models.Machines;
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

            await Assert.ThrowsAsync<KeyNotFoundException>(() => (machineService.GetMachineById(machineId)));
        }

        [Fact]
        public async void EnsureMachineIsFoundByMachineType()
        {
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedMachines(context);
            var machineService = new MachineService(context);

            List<Machine> result = await machineService.GetMachineByType(new Guid("21111111-1111-1111-1111-111111111111"));

            foreach (Machine m in result)
            {
                if (m.MachineType.Id.Equals(new Guid("21111111-1111-1111-1111-111111111111")))
                {
                    Assert.True(/* expectedMachineModel.Equals(result.MachineModel.Model) */result.Contains(m));

                }
            }







        }

        [Fact]
        public async void EnsureMachineTypeIsUpdated()
        {
            var context = ContextMocker.GetContextMock();
            ContextMocker.SeedMachines(context);

            var machineService = new MachineService(context);
            var machineTypeService = new MachineTypeService(context);

            Guid machineIdToBeUpdated = new Guid("11111111-1111-1111-1111-111111111111");
            string idNewMachineType = "31111111-1111-1111-1111-111111111111";
            await machineService.UpdateMachineType(machineIdToBeUpdated, idNewMachineType);

            var result = await machineService.GetMachineById(machineIdToBeUpdated);

            Assert.True(result.MachineType.Id.Equals(idNewMachineType));
        }
    }
}