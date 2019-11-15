using System;
using System.Collections.Generic;

namespace MasterDataFactory.Models.ProductionLines
{
    public class ProductionLineDescription : ValueObject
    {
        public string Description { get; set; }

        protected ProductionLineDescription(){

        }

        public ProductionLineDescription(string value)
        {
            Validate(value);
            Description = value;
        }

        private void Validate(string value)
        {
            if(value == null)
                throw new ArgumentException("Description is needed.");
            if(value.Equals(""))
                throw new ArgumentException("Description can't be empty.");
                
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Description;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            ProductionLineDescription ProductionLineDescription = (ProductionLineDescription)obj;
            return Description.Equals(ProductionLineDescription.Description);
        }
        
        public override int GetHashCode()
        {
            return Description.GetHashCode();
        }
    }
}