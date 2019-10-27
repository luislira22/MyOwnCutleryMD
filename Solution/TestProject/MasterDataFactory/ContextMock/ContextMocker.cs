﻿using System;
using MasterDataFactory.Models.Machines;
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
            dbContext.Operations.Add(operation1);
        }

        public static void SeedMachines(Context dbContext)
        {
            //SeedMachineTypes();

            var machine1 = new Machine
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                MachineType = null, //Preencher com machineType
                MachineBrand = new MachineBrand("Siemens"),
                MachineModel = new MachineModel("HO-501"),
                MachineLocation = new MachineLocation("Sector 10")
            };

            dbContext.Machines.Add(machine1);
        }
    }
}