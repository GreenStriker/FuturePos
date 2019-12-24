using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{

    public interface IOvertimeService : IServiceBase<Overtime>
    {
        Task<IEnumerable<Overtime>> GetUsers(int p_orgId);
        Task<Overtime> GetUser(int id);
    }

    public class OvertimeService : ServiceBase<Overtime>, IOvertimeService
    {
        public IOvertimeRepository _repository { get; }
        public OvertimeService(IOvertimeRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Overtime>> GetUsers(int p_orgId)
        {
            return await _repository.GetUsers(p_orgId);
        }
        public async Task<Overtime> GetUser(int id)
        {
            return await _repository.GetUser(id);
        }
    }
}
