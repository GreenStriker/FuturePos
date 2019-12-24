using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{

    public interface IUserService : IServiceBase<User>
    {
        Task<IEnumerable<User>> GetUsers(int p_orgId);
        Task<User> GetUser(int id);
    }

    public class UserService : ServiceBase<User>, IUserService
    {
        public IUserRepository _repository { get; }
        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<User>> GetUsers(int p_orgId)
        {
            return await _repository.GetUsers(p_orgId);
        }
        public async Task<User> GetUser(int id)
        {
            return await _repository.GetUser(id);
        }
    }
}
