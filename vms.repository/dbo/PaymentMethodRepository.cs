using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using vms.entity.models;
using vms.entity.viewModels;
using vms.repository;

namespace vms.repository.dbo
{
    public interface IPaymentMethodRepository : IRepositoryBase<PaymentMethod>
    {
        Task<IEnumerable<PaymentMethod>> GetAll();
        Task<PaymentMethod> GetById(int id);
    }
    public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public PaymentMethodRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<PaymentMethod>> GetAll()
        {
            var vats = await this.Query().SelectAsync();

            return vats;
        }

        public async Task<PaymentMethod> GetById(int id)
        {

            var vats = await this.Query().SingleOrDefaultAsync(c=>c.PaymentMethodId == id,CancellationToken.None);

            return vats;
        }
    }
}
