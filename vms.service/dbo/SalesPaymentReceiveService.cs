using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.entity.viewModels;
using vms.repository.dbo;

namespace vms.service.dbo
{
   
    public interface ISalesPaymentReceiveService : IServiceBase<SalePayment>
    {
        Task<bool> ManageSalesDueAsync(VmSalesPaymentReceive vmSales);
    }

    public class SalesPaymentReceiveService : ServiceBase<SalePayment>, ISalesPaymentReceiveService
    {
        public ISalesPaymentReceiveRepository _repository { get; }
        public SalesPaymentReceiveService(ISalesPaymentReceiveRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> ManageSalesDueAsync(VmSalesPaymentReceive vmSales)
        {
            return await _repository.ManageSalesDueAsync(vmSales);
        }
    }
}
