using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IPurchaseContentService : IServiceBase<PurchaseContent>
    {
        Task<IEnumerable<PurchaseContent>> GetAll();
        Task<PurchaseContent> GetById(int id);
    }
    public class PurchaseContentService : ServiceBase<PurchaseContent>, IPurchaseContentService
    {
        public IPurchaseContentRepository _repository { get; }
        public PurchaseContentService(IPurchaseContentRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PurchaseContent>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<PurchaseContent> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
