using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{

    public interface IBranchService : IServiceBase<Branch>
    {
        Task<IEnumerable<Branch>> GetUsers(int p_orgId);
        Task<Branch> GetUser(int id);
    }

    public class BranchService : ServiceBase<Branch>, IBranchService
    {
        public IBranchRepository _repository { get; }
        public BranchService(IBranchRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Branch>> GetUsers(int p_orgId)
        {
            return await _repository.GetUsers(p_orgId);
        }
        public async Task<Branch> GetUser(int id)
        {
            return await _repository.GetUser(id);
        }
    }
}
