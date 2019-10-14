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
        public async Task<OperationDTO> getOperation(long id){
            var operation = await _context.Operations.FindAsync(id);
            return new OperationDTO(operation.Id,operation.Cod,operation.Name);
        }
        public async void postOperation(Operation operation){
            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();
        }
    }
}