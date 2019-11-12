using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MasterDataProduct.Models.Products
{
    public class ManufacturingPlan
    {

        public ICollection<Guid> operationIDs;

        public ManufacturingPlan()
        {
            operationIDs = new Collection<Guid>();
        }
        
        public ManufacturingPlan(ICollection<string> guidStrings)
        {
            foreach (var guidString in guidStrings)
            {
                Guid tmp;
                if(Guid.TryParse(guidString,out tmp))
                    operationIDs.Add(tmp);
                throw new FormatException("invalid Guid format.");
            }
        }

        public void AddOperationId(Guid id)
        {
            operationIDs.Add(id);
        }

       
    }
}