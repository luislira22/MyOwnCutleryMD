using System;
using System.Collections.Generic;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Services;
using Xunit;

namespace TestProject.MasterDataFactory.Services
{
    public class OperationServicesTest
    {
        [Fact]
        public async void EnsureGettingOpperationByIdFound()
        {
            Guid operationId = new Guid("12345678-1234-1234-1234-123412341234");

            //mock
            Context context = ContextMocker.GetContextMock();
            ContextMocker.SeedOperations(context);
            
            OperationService service = new OperationService(context);
            Operation result = await service.GetOperationById(operationId);

            string expectedDescription = "Triturar";
            TimeSpan expectedDuration = new TimeSpan(0,20,10);
            
            Assert.True(expectedDescription.Equals(result.Description.Description));
            Assert.True(expectedDuration.Equals(result.Duration.Duration));
        }

        [Fact]
        public void EnsureGettingOperationByIdNotFound()
        {
            Guid operationId = new Guid("22345678-1234-1234-1234-123412341234");

            //mock
            Context context = ContextMocker.GetContextMock();
            ContextMocker.SeedOperations(context);
            
            OperationService service = new OperationService(context);

            Assert.ThrowsAsync<KeyNotFoundException>(() =>(service.GetOperationById(operationId)));
        }

        [Fact]
        public void EnsureOperationToDeleteNotFound()
        {
            Guid operationId = new Guid("22345678-1234-1234-1234-123412341234");
            
            //mock 
            Context context = ContextMocker.GetContextMock();
            ContextMocker.SeedOperations(context);
            
            OperationService service = new OperationService(context);
            Assert.ThrowsAsync<KeyNotFoundException>(() =>(service.DeleteOperationById(operationId)));
        }
    }
}