
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IIncentiveService : IServiceBase<Incentive>
    {
        Task<IEnumerable<Incentive>> GetAll();
        Task<Incentive> GetById(int id);
    }
    public class IncentiveService : ServiceBase<Incentive>, IIncentiveService
    {
        public IIncentiveRepository _repository { get; }
        public IncentiveService(IIncentiveRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Incentive>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Incentive> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
