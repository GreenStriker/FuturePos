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
    public interface IIncentiveRepository : IRepositoryBase<Incentive>
    {
        Task<IEnumerable<Incentive>> GetAll();
        Task<Incentive> GetById(int id);
    }
    public class IncentiveRepository : RepositoryBase<Incentive>, IIncentiveRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public IncentiveRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Incentive>> GetAll()
        {
            var Incentives = await this.Query().SelectAsync();

            return Incentives;
        }

        public async Task<Incentive> GetById(int id)
        {

            var Incentives = await this.Query().SingleOrDefaultAsync(c=>c.IncentiveId==id,CancellationToken.None);

            return Incentives;
        }
    }
}
