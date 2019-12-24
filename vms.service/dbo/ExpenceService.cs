
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface IExpenceService : IServiceBase<Expence>
    {
        Task<IEnumerable<Expence>> GetAll();
        Task<Expence> GetById(int id);
    }
    public class ExpenceService : ServiceBase<Expence>, IExpenceService
    {
        public IExpenceRepository _repository { get; }
        public ExpenceService(IExpenceRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Expence>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Expence> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
