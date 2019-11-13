using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MasterDataProduct.Models.Products
{
    public class ManufacturingPlan
    {

        public Guid Id { get; set; }
        
        public virtual ICollection<OperationId> Ids { get; set; }

        public int CurrentIndex;

        public ManufacturingPlan()
        {
            Ids = new Collection<OperationId>();
            CurrentIndex = 0;
        }

        
        public void AddOperationId(string id)
        {
            Ids.Add(new OperationId(id,CurrentIndex));
            CurrentIndex++;
        }
        
    }
}