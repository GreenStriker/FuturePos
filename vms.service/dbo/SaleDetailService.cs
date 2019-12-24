using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface ISaleDetailService : IServiceBase<SalesDetail>
    {
        Task<IEnumerable<SalesDetail>> GetAll();
        Task<SalesDetail> GetById(int id);
    }
    public class SaleDetailService : ServiceBase<SalesDetail>, ISaleDetailService
    {
        public ISaleDetailRepository _repository { get; }
        public SaleDetailService(ISaleDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<SalesDetail>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<SalesDetail> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
