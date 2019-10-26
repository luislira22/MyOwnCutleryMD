using System;
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
            Operation operation1 = new Operation(new Guid("12345678-1234-1234-1234-123412341234"),"Triturar",new TimeSpan(0,20,10));
            dbContext.Operations.Add(operation1);
        }
    }
}