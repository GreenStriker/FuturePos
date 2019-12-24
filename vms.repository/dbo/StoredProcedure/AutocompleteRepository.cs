using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.StoredProcedureModel;

namespace vms.repository.dbo.StoredProcedure
{
    public interface IAutocompleteRepository
    {
        Task<List<SpGetProductAutocompleteForSale>> GetProductAutocompleteForSales(int branchId,
            string searchTerm);
    }

    public class AutocompleteRepository : IAutocompleteRepository
    {
        private readonly DbContext _context;

        public AutocompleteRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<List<SpGetProductAutocompleteForSale>> GetProductAutocompleteForSales(int branchId, string searchTerm)
        {
            try
            {
                return await _context.Set<SpGetProductAutocompleteForSale>().FromSql("SpGetProductAutocompleteForSale @BranchId={0}, @ProductSearchTerm={1}", branchId, searchTerm).ToListAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
