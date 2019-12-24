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
    public interface IPayrollRepository : IRepositoryBase<Payroll>
    {
        Task<IEnumerable<Payroll>> GetAll();
        Task<Payroll> GetById(int id);
    }
    public class PayrollRepository : RepositoryBase<Payroll>, IPayrollRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public PayrollRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Payroll>> GetAll()
        {
            var Payrolls = await this.Query().SelectAsync();

            return Payrolls;
        }

        public async Task<Payroll> GetById(int id)
        {

            var Payrolls = await this.Query().SingleOrDefaultAsync(c=>c.PayrollId==id,CancellationToken.None);

            return Payrolls;
        }
    }
}
