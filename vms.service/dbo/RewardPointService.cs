
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IRewardPointService : IServiceBase<RewardPoint>
    {
        Task<IEnumerable<RewardPoint>> GetAll();
        Task<RewardPoint> GetById(int id);
    }
    public class RewardPointService : ServiceBase<RewardPoint>, IRewardPointService
    {
        public IRewardPointRepository _repository { get; }
        public RewardPointService(IRewardPointRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<RewardPoint>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<RewardPoint> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
