using System;
using System.Collections.Generic;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.MachineTypesOperations;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Models.PersistenceContext;
using Microsoft.EntityFrameworkCore;

namespace TestProject.MasterDataFactory
{
    public class ContextMocker
    {

        public static Context GetContextMock()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("TestDB")
                .Options;
            Context dbContext = new Context(options);
            return dbContext;
        }

        public static void SeedOperations(Context dbContext)
        {
            Operation operation1 = new Operation(new Guid("12345678-1234-1234-1234-123412341234"), "Triturar",
                new TimeSpan(0, 20, 10));
            Operation operation2 = new Operation(new Guid("12345678-1234-1234-1234-123412341235"), "Martelar",
                new TimeSpan(0, 30, 10));
            dbContext.Operations.Add(operation1);
            dbContext.Operations.Add(operation2);
        }

        public static void SeedMachineTypes(Context dbContext)
        {
            SeedOperations(dbContext);
            Operation op = dbContext.Operations.FindAsync(new Guid("12345678-1234-1234-1234-123412341234")).Result;
            Operation op2 = dbContext.Operations.FindAsync(new Guid("12345678-1234-1234-1234-123412341235")).Result;
            List<Operation> ops = new List<Operation>() { op };
            List<Operation> ops2 = new List<Operation>() { op2 };

            MachineType machineType = new MachineType(new MachineTypeDescription("Trituradora"), ops);
            machineType.Id = new Guid("21111111-1111-1111-1111-111111111111");
            
            MachineType machineType2 = new MachineType(new MachineTypeDescription("Martelador"), ops2);
            machineType2.Id = new Guid("31111111-1111-1111-1111-111111111111");

            dbContext.MachineTypes.Add(machineType);
        }

        public static void SeedMachines(Context dbContext)
        {
            SeedMachineTypes(dbContext);
            MachineType machineType = dbContext.MachineTypes.FindAsync(new Guid("21111111-1111-1111-1111-111111111111")).Result;

            var machine1 = new Machine
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                MachineType = machineType, //Preencher com machineType
                MachineBrand = new MachineBrand("Siemens"),
                MachineModel = new MachineModel("HO-501"),
                MachineLocation = new MachineLocation("Sector 10")
            };

            dbContext.Machines.Add(machine1);
        }


    }
}