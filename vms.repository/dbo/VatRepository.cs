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
    public interface IVatRepository : IRepositoryBase<Vat>
    {
        Task<IEnumerable<Vat>> GetAll();
        Task<Vat> GetById(int id);
    }
    public class VatRepository : RepositoryBase<Vat>, IVatRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public VatRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Vat>> GetAll()
        {
            var vats = await this.Query().SelectAsync();

            return vats;
        }

        public async Task<Vat> GetById(int id)
        {

            var vats = await this.Query().SingleOrDefaultAsync(c=>c.VatId==id,CancellationToken.None);

            return vats;
        }
    }
}
