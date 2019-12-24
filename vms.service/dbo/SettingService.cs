
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface ISettingService : IServiceBase<Setting>
    {
        Task<IEnumerable<Setting>> GetAll();
        Task<Setting> GetById(int id);
    }
    public class SettingService : ServiceBase<Setting>, ISettingService
    {
        public ISettingRepository _repository { get; }
        public SettingService(ISettingRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Setting>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Setting> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
