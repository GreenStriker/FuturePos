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
    public interface ISalaryRepository : IRepositoryBase<Salary>
    {
        Task<IEnumerable<Salary>> GetAll();
        Task<Salary> GetById(int id);
    }
    public class SalaryRepository : RepositoryBase<Salary>, ISalaryRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public SalaryRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Salary>> GetAll()
        {
            var Salarys = await this.Query().SelectAsync();

            return Salarys;
        }

        public async Task<Salary> GetById(int id)
        {

            var Salarys = await this.Query().SingleOrDefaultAsync(c=>c.SalaryId==id,CancellationToken.None);

            return Salarys;
        }
    }
}
