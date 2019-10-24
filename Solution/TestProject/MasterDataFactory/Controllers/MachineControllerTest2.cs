using System;
using System.Collections.Generic;
using MasterDataFactory.Controllers;
using MasterDataFactory.Models.Domain.MachineTypes;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.Operations;
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
           /* Guid id = Guid.NewGuid();
            IList<Operation> operations = new List<Operation>();
            Operation operation1 = new Operation
            {
                Id = id,
                Description = new OperationDescription("Description")
            };
            operations.Add(operation1);

            IList<MachineType> machineTypes = new List<MachineType>();
            List<Operation> operationlist = new List<Operation>();
            MachineType machineType = new MachineType
            {
                Id = Guid.NewGuid(),
                Type = "Machine type",
                Operations = operationlist
            };

            //IList<Machine> machines = new List<Machine>();
            var context = new Mock<Context>();
            context.Setup(o => o.Operations).ReturnsDbSet(operations);
            context.Setup(o => o.MachineTypes).ReturnsDbSet(machineTypes);

            OperationController operationController = new OperationController(context.Object);

            await operationController.GetOperation(id);*/

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