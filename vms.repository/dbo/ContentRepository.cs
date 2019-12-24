using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using vms.entity.models;
using vms.entity.viewModels;
using vms.repository;

namespace vms.repository.dbo
{
    public interface IContentRepository : IRepositoryBase<Content>
    {
        Task<IEnumerable<Content>> GetAll();
        Task<Content> GetById(int id);
    }
    public class ContentRepository : RepositoryBase<Content>, IContentRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public ContentRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Content>> GetAll()
        {
            var contents = await this.Query().SelectAsync();

            return contents;
        }

        public async Task<Content> GetById(int id)
        {
            var contents = await this.Query().SingleOrDefaultAsync(p => p.ContentId == id, CancellationToken.None);

            return contents;
        }
    }
}
