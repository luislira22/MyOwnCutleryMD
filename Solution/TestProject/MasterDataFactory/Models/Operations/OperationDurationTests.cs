using System;
using MasterDataFactory.DTO.Operations;
using MasterDataFactory.Models.Operations;
using Xunit;

namespace TestProject.MasterDataFactory.Models.Operations
{
    public class OperationDurationTests
    {
        [Fact]
        public void EnsureTimeSpanPFailsParse()
        {
            string duration = "10,00:00";

            OperationDTO operationDTO = new OperationDTO(new Guid(), "description",duration,"50mm");

            Assert.Throws<FormatException>(() => new Operation(operationDTO));
        }
    }
}