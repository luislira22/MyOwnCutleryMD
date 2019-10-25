using System;
using System.Data.SqlClient;
using MasterDataFactory.Models.PersistenceContext;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Repositories;
using MasterDataFactory.Repositories.Impl;
using MasterDataFactory.Repositories.Interfaces;

namespace MasterDataFactory.Services
{
    public class OperationService
    {
        private readonly IOperationRepository _operationRepository;
        public OperationService(Context _context){
           _operationRepository = new OperationRepository(_context);
        }
        
        public async Task<Operation> getOperationById(Guid id){
            return await _operationRepository.GetById(id);
        }
        public async Task postOperation(Operation operation)
        {
            await _operationRepository.Create(operation);
        }

        public async Task deleteOperation(Guid id)
        {
            await _operationRepository.Delete(id);
        }

        public async Task<bool> existsOperation(Guid id)
        {
            return await _operationRepository.Exists(id);
        }
    }
}