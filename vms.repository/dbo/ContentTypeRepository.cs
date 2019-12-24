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
    public interface IContentTypeRepository : IRepositoryBase<Contenttype>
    {
        Task<IEnumerable<Contenttype>> GetAll();
        Task<Contenttype> GetById(int id);
    }
    public class ContentTypeRepository : RepositoryBase<Contenttype>, IContentTypeRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public ContentTypeRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Contenttype>> GetAll()
        {
            var contents = await this.Query().SelectAsync();

            return contents;
        }

        public async Task<Contenttype> GetById(int id)
        {
            var contents = await this.Query().SingleOrDefaultAsync(p => p.ContentTypeId == id, CancellationToken.None);

            return contents;
        }
    }
}
