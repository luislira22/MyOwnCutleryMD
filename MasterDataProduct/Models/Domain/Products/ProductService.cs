using System.Threading.Tasks;
using MasterDataProduct.Models.PersistenceContext;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataProduct.Models.Domain.Products
{
    public class ProductService
    {
        private readonly Context _context;

        public ProductService(Context context)
        {
            _context = context;
        }


        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            /*MachineId machineId = new MachineId(1);
            _context.MachineIds.Add(machineId);
            machine.MachineId = machineId;*/
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            //return CreatedAtAction(nameof(machine), new { id = machine.Id,  }, machine);
            return null;
        }
    }
}