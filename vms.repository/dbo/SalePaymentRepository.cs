using Microsoft.AspNetCore.DataProtection;
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
    public interface ISalePaymentRepository : IRepositoryBase<SalePayment>
    {
        Task<IEnumerable<SalePayment>> GetAll();
        Task<SalePayment> GetById(int id);
        Task<bool> ManageSalesDueAsync(VmSalesPaymentReceive vmSales);

    }
    public class SalePaymentRepository : RepositoryBase<SalePayment>, ISalePaymentRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public SalePaymentRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<SalePayment>> GetAll()
        {
            var data = await this.Query().SelectAsync();

            return data;
        }
        public async Task<SalePayment> GetById(int ids)
        {
            int id = ids;
            var data = await this.Query().SingleOrDefaultAsync(x => x.SalePaymentId == id, System.Threading.CancellationToken.None);
            return data;
        }
        public async Task<bool> ManageSalesDueAsync(VmSalesPaymentReceive vmSales)
        {
            try
            {
                this._context.Database.ExecuteSqlCommand(
                    $"EXEC [dbo].[SPManageSalesDue]" +
                    $"@SalesId" +
                    $",@PaymentMethodId" +
                    $",@TotalPaidAmount" +
                    $",@PaidAmount" +
                    $",@CreatedBy "

                    , new SqlParameter("@SalesId", vmSales.SalesId)
                    , new SqlParameter("@PaymentMethodId", vmSales.PaymentMethodId)
                    , new SqlParameter("@TotalPaidAmount", vmSales.TotalPaidAmount)
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
