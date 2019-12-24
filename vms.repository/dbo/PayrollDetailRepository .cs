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
    public interface IPayrollDetailRepository : IRepositoryBase<PayrollDetail>
    {
        Task<IEnumerable<PayrollDetail>> GetAll();
        Task<PayrollDetail> GetById(int id);
    }
    public class PayrollDetailRepository : RepositoryBase<PayrollDetail>, IPayrollDetailRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public PayrollDetailRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<PayrollDetail>> GetAll()
        {
            var PayrollDetails = await this.Query().SelectAsync();

            return PayrollDetails;
        }

        public async Task<PayrollDetail> GetById(int id)
        {

            var PayrollDetails = await this.Query().SingleOrDefaultAsync(c=>c.PayrollDetailsId==id,CancellationToken.None);

            return PayrollDetails;
        }
    }
}
