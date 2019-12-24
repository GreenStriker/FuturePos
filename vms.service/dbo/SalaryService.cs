
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface ISalaryService : IServiceBase<Salary>
    {
        Task<IEnumerable<Salary>> GetAll();
        Task<Salary> GetById(int id);
    }
    public class SalaryService : ServiceBase<Salary>, ISalaryService
    {
        public ISalaryRepository _repository { get; }
        public SalaryService(ISalaryRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Salary>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Salary> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
