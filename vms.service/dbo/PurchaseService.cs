using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IPurchaseService : IServiceBase<Purchase>
    {
        Task<IEnumerable<Purchase>> GetAll();
        Task<Purchase> GetById(int id);
    }
    public class PurchaseService : ServiceBase<Purchase>, IPurchaseService
    {
        public IPurchaseRepository _repository { get; }
        public PurchaseService(IPurchaseRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Purchase>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Purchase> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
