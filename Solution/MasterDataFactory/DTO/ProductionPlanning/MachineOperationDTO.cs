namespace MasterDataFactory.DTO.ProductionPlanning
{
    public class MachineOperationDTO
    {
        public string OperationType { get; set; }
        public string Machine { get; set;}
        public string Tool { get;set;}
        public double SetupTime { get; set;}
        public double ExecutionTime { get; set;}
    }
}