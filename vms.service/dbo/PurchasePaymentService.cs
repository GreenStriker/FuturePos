using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.entity.viewModels;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IPurchasePaymentService : IServiceBase<PurchasePayment>
    {
        Task<IEnumerable<PurchasePayment>> GetAll();
        Task<PurchasePayment> GetById(int id);
        Task<bool> ManagePurchaseDue(vmPurchasePayment vmPurchase);
    }
    public class PurchasePaymentService : ServiceBase<PurchasePayment>, IPurchasePaymentService
    {
        public IPurchasePaymentRepository _repository { get; }
        public PurchasePaymentService(IPurchasePaymentRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PurchasePayment>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<PurchasePayment> GetById(int id)
        {
            return await _repository.GetById(id);
        }
        public async Task<bool> ManagePurchaseDue(vmPurchasePayment vmPurchase)
        {
            return await _repository.ManagePurchaseDue(vmPurchase);
        }
    }
}
