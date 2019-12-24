using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{

    public interface IAdvancedSalaryService : IServiceBase<AdvancedSalary>
    {
        Task<IEnumerable<AdvancedSalary>> GetUsers(int p_orgId);
        Task<AdvancedSalary> GetUser(int id);
    }

    public class AdvancedSalaryService : ServiceBase<AdvancedSalary>, IAdvancedSalaryService
    {
        public IAdvancedSalaryRepository _repository { get; }
        public AdvancedSalaryService(IAdvancedSalaryRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<AdvancedSalary>> GetUsers(int p_orgId)
        {
            return await _repository.GetUsers(p_orgId);
        }
        public async Task<AdvancedSalary> GetUser(int id)
        {
            return await _repository.GetUser(id);
        }
    }
}
