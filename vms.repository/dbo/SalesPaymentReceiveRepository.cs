using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.entity.viewModels;

namespace vms.repository.dbo
{
  
    public interface ISalesPaymentReceiveRepository : IRepositoryBase<SalePayment>
    {
        Task<bool> ManageSalesDueAsync(VmSalesPaymentReceive vmSales);
    }
    public class SalesPaymentReceiveRepository : RepositoryBase<SalePayment>, ISalesPaymentReceiveRepository
    {
        private readonly DbContext _context;

        public SalesPaymentReceiveRepository(DbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> ManageSalesDueAsync(VmSalesPaymentReceive vmSales)
        {
            try
            {
                this._context.Database.ExecuteSqlCommand(
                    $"EXEC [dbo].[SPManageSalesDue]" +
                    $"@SalesId" +
                    $",@PaymentMethodId" +
                    $",@PaidAmount" +
                    $",@CreatedBy "

                    , new SqlParameter("@SalesId", vmSales.SalesId)
                    , new SqlParameter("@PaymentMethodId", vmSales.PaymentMethodId)
                    , new SqlParameter("@PaidAmount", vmSales.PaidAmount)
                    , new SqlParameter("@CreatedBy", vmSales.CreatedBy)
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return await Task.FromResult(true);
        }
    }
}
