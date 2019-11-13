using System;
using MasterDataProduct.DTO.Products;

namespace MasterDataProduct.Models.Products
{
    public class OperationId
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public int Index { get; set; }
        
        //public ManufacturingPlan  ManufacturingPlan {get; set; }
        
        public OperationId(string value,int index)
        {
            Value = value;
            Index = index;
        }

        public OperationId(string value)
        {
            Value = value;
        }

        public OperationId(OperationIdDTO dto){
            this.Value = dto.Id;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            OperationId operationId = (OperationId)obj;
            return Value.Equals(operationId.Value);
        }
        
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public string toString(){
            return Id.ToString();
        }

        public OperationIdDTO toDTO(){
            return new OperationIdDTO(Value);
        }
    }
}