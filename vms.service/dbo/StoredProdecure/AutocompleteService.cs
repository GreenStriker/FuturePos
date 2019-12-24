using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.StoredProcedureModel;
using vms.repository.dbo.StoredProcedure;

namespace vms.service.dbo.StoredProdecure
{
    public interface IAutocompleteService
    {
        Task<List<SpGetProductAutocompleteForSale>> GetProductAutocompleteForSales(int branchId,
            string searchTerm);

    
    }
    public class AutocompleteService : IAutocompleteService
    {
        private readonly IAutocompleteRepository _autocompleteRepository;

        public AutocompleteService(IAutocompleteRepository autocompleteRepository)
        {
            _autocompleteRepository = autocompleteRepository;
        }

        public async Task<List<SpGetProductAutocompleteForSale>> GetProductAutocompleteForSales(int branchId, string searchTerm)
        {
            return await _autocompleteRepository.GetProductAutocompleteForSales(branchId, searchTerm);
        }
    }
}
