using System;
using System.Data.SqlClient;
using MasterDataFactory.Models.PersistenceContext;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Repositories;

namespace MasterDataFactory.Services
{
    public class OperationService
    {
        private readonly IOperationRepository _operationRepository;
        public OperationService(Context _context){
           _operationRepository = new OperationRepository(_context);
        }
        
        public async Task<Operation> getOperation(Guid id){
            return await _operationRepository.GetById(id);
        }
        public async void postOperation(Operation operation){
            await _operationRepository.Create(operation);
            await _operationRepository.SaveChangesAsync();
        }

        
    }
}