
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface IPaymentMethodService : IServiceBase<PaymentMethod>
    {
        Task<IEnumerable<PaymentMethod>> GetAll();
        Task<PaymentMethod> GetById(int id);
    }
    public class PaymentMethodService : ServiceBase<PaymentMethod>, IPaymentMethodService
    {
        public IPaymentMethodRepository _repository { get; }
        public PaymentMethodService(IPaymentMethodRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PaymentMethod>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<PaymentMethod> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
