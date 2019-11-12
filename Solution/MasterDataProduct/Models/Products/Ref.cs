using System.Collections.Generic;

namespace MasterDataProduct.Models.Products
{
    public class Ref : ValueObject
    {
        public string Value { get; set; }

        public Ref(string value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            Ref refTmp = (Ref)obj;
            return Value.Equals(refTmp.Value);
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}