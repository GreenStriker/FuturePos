using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using vms.entity.models;
using vms.entity.viewModels;

namespace vms.repository.dbo
{
    public interface IPurchaseRepository : IRepositoryBase<Purchase>
    {
        Task<IEnumerable<Purchase>> GetAll();
        Task<Purchase> GetById(int id);
        
    }
    public class PurchaseRepository : RepositoryBase<Purchase>, IPurchaseRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public PurchaseRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Purchase>> GetAll()
        {
            var data = await this.Query().SelectAsync();

            return data;
        }
        public async Task<Purchase> GetById(int ids)
        {
            int id = ids;
            var data = await this.Query().Include("PurchaseDetails.Product.Munit")
                .Include("PurchasePayments.PaymentMethod")
                .Include(c=>c.PurchaseContents)
                .Include(c=>c.Vendor)
                .Include(c=>c.Branch)
                .SingleOrDefaultAsync(x => x.PurchaseId == id, System.Threading.CancellationToken.None);
            return data;
        }
   
    }
}
