using System;
using System.Data.SqlClient;
using MasterDataFactory.Models.PersistenceContext;
using System.Threading.Tasks;

namespace MasterDataFactory.Models.Domain.Operations
{
    public class OperationService
    {
        private readonly Context _context;
        public OperationService(Context _context){
           this._context = _context;
        }
        
        public async Task<OperationDTO> getOperation(Guid id){
            var obj = await _context.Operations.FindAsync(id);
            if (obj == null)
            {
                return null;
            }
            return new OperationDTO(id,obj.Description.Description);
        }
        public Boolean postOperation(Operation operation){
            try
            {
                _context.Operations.Add(operation);
                _context.SaveChangesAsync();
                return true;
            }
            catch (SqlException sqlException)
            {
                return false;
            }
        }
    }
}