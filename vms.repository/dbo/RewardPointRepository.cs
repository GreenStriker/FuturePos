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
    public interface IRewardPointRepository : IRepositoryBase<RewardPoint>
    {
        Task<IEnumerable<RewardPoint>> GetAll();
        Task<RewardPoint> GetById(int id);
    }
    public class RewardPointRepository : RepositoryBase<RewardPoint>, IRewardPointRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public RewardPointRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<RewardPoint>> GetAll()
        {
            var RewardPoints = await this.Query().SelectAsync();

            return RewardPoints;
        }

        public async Task<RewardPoint> GetById(int id)
        {

            var RewardPoints = await this.Query().SingleOrDefaultAsync(c=>c.RewardPoinId==id,CancellationToken.None);

            return RewardPoints;
        }
    }
}
