namespace MasterDataFactory.Models.Domain.Operations
{
    public class OperationDTO
    {
        public long id {get;set;} 
        
        public string cod{get;set;}
        
        public string name{get;set;}

        public OperationDTO(long id,string cod,string name){
            this.id = id;
            this.cod = cod;
            this.name = name;
        }
    }
}