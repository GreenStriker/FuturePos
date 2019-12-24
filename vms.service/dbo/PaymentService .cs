
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface IPaymentService : IServiceBase<Payment>
    {
        Task<IEnumerable<Payment>> GetAll();
        Task<Payment> GetById(int id);
    }
    public class PaymentService : ServiceBase<Payment>, IPaymentService
    {
        public IPaymentRepository _repository { get; }
        public PaymentService(IPaymentRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Payment>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Payment> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
