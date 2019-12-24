
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IPayrollDetailService : IServiceBase<PayrollDetail>
    {
        Task<IEnumerable<PayrollDetail>> GetAll();
        Task<PayrollDetail> GetById(int id);
    }
    public class PayrollDetailService : ServiceBase<PayrollDetail>, IPayrollDetailService
    {
        public IPayrollDetailRepository _repository { get; }
        public PayrollDetailService(IPayrollDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PayrollDetail>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<PayrollDetail> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
