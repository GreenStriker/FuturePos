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
    public interface ISettingRepository : IRepositoryBase<Setting>
    {
        Task<IEnumerable<Setting>> GetAll();
        Task<Setting> GetById(int id);
    }
    public class SettingRepository : RepositoryBase<Setting>, ISettingRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public SettingRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Setting>> GetAll()
        {
            var Settings = await this.Query().SelectAsync();

            return Settings;
        }

        public async Task<Setting> GetById(int id)
        {

            var Settings = await this.Query().SingleOrDefaultAsync(c=>c.SettingsId==id,CancellationToken.None);

            return Settings;
        }
    }
}
