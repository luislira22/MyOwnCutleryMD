using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterDataFactory.Models.Domain.Operations
{
    public class Operation
    {
        public long Id { get; set; }
        public OperationId OperationId { get; set; }
    }
}
