using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface ISaleService : IServiceBase<Sale>
    {
        Task<IEnumerable<Sale>> GetAll();
        Task<Sale> GetById(int id);
    }
    public class SaleService : ServiceBase<Sale>, ISaleService
    {
        public ISaleRepository _repository { get; }
        public SaleService(ISaleRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Sale>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Sale> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
