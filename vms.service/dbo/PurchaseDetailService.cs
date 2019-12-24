using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IPurchaseDetailService : IServiceBase<PurchaseDetail>
    {
        Task<IEnumerable<PurchaseDetail>> GetAll();
        Task<PurchaseDetail> GetById(int id);
    }
    public class PurchaseDetailService : ServiceBase<PurchaseDetail>, IPurchaseDetailService
    {
        public IPurchaseDetailRepository _repository { get; }
        public PurchaseDetailService(IPurchaseDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PurchaseDetail>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<PurchaseDetail> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
