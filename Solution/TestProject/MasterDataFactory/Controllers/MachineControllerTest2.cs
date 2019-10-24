using System;
using System.Collections.Generic;
using MasterDataFactory.Controllers;
using MasterDataFactory.Models.Domain.MachineTypes;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.PersistenceContext;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.MasterDataFactory.Controllers
{
    public class MachineControllerTest2
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public MachineControllerTest2(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async void Test1()
        {    
           /* IList<Machine> machines = new List<Machine>();
            Machine machine1 = new Machine(null);
            machine1.Id = new Guid();
            machine1.MachineType = new MachineType();
            machines.Insert(0, machine1);
            var userContextMock = new Mock<Context>();
            userContextMock.Setup(x => x.Machines).ReturnsDbSet(machines);
            var machineController = new MachineController(userContextMock.Object);
            var result = await machineController.GetMachines();*/
        }
    }
}