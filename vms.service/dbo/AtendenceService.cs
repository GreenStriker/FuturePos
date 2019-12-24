
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface IAtendenceService : IServiceBase<Atendence>
    {
        Task<IEnumerable<Atendence>> GetAll();
        Task<Atendence> GetById(int id);
    }
    public class AtendenceService : ServiceBase<Atendence>, IAtendenceService
    {
        public IAtendenceRepository _repository { get; }
        public AtendenceService(IAtendenceRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Atendence>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Atendence> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
