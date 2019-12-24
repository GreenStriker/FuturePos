
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IPayrollService : IServiceBase<Payroll>
    {
        Task<IEnumerable<Payroll>> GetAll();
        Task<Payroll> GetById(int id);
    }
    public class PayrollService : ServiceBase<Payroll>, IPayrollService
    {
        public IPayrollRepository _repository { get; }
        public PayrollService(IPayrollRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Payroll>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Payroll> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
