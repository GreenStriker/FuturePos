
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface IEmployeService : IServiceBase<Employe>
    {
        Task<IEnumerable<Employe>> GetAll();
        Task<Employe> GetById(int id);
    }
    public class EmployeService : ServiceBase<Employe>, IEmployeService
    {
        public IEmployeRepository _repository { get; }
        public EmployeService(IEmployeRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Employe>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Employe> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
