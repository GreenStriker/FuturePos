using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface ISaleContentService : IServiceBase<SaleContent>
    {
        Task<IEnumerable<SaleContent>> GetAll();
        Task<SaleContent> GetById(int id);
    }
    public class SaleContentService : ServiceBase<SaleContent>, ISaleContentService
    {
        public ISaleContentRepository _repository { get; }
        public SaleContentService(ISaleContentRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<SaleContent>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<SaleContent> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
