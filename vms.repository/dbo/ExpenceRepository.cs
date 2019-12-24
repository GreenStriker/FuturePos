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
    public interface IExpenceRepository : IRepositoryBase<Expence>
    {
        Task<IEnumerable<Expence>> GetAll();
        Task<Expence> GetById(int id);
    }
    public class ExpenceRepository : RepositoryBase<Expence>, IExpenceRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public ExpenceRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Expence>> GetAll()
        {
            var Expences = await this.Query().SelectAsync();

            return Expences;
        }

        public async Task<Expence> GetById(int id)
        {

            var Expences = await this.Query().SingleOrDefaultAsync(c=>c.ExpenceId==id,CancellationToken.None);

            return Expences;
        }
    }
}
