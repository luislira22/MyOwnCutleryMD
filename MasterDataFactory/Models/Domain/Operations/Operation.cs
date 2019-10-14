
namespace MasterDataFactory.Models.Domain.Operations
{
    public class Operation
    {

        public long Id {get;set;} 
        public string Cod{get;set;}
        public string Name{get;set;}

        public Operation(){

        }
        public Operation(string Cod, string Name){
            this.Cod = Cod;
            this.Name = Name;
        }
    }
}
