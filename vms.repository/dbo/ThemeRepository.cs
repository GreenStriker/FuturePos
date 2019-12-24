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
    public interface IThemeRepository : IRepositoryBase<Theme>
    {
        Task<IEnumerable<Theme>> GetAll();
        Task<Theme> GetById(int id);
    }
    public class ThemeRepository : RepositoryBase<Theme>, IThemeRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public ThemeRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Theme>> GetAll()
        {
            var Themes = await this.Query().SelectAsync();

            return Themes;
        }

        public async Task<Theme> GetById(int id)
        {

            var Themes = await this.Query().SingleOrDefaultAsync(c=>c.ThemeId==id,CancellationToken.None);

            return Themes;
        }
    }
}
