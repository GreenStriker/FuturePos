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
    public interface IVendorRepository : IRepositoryBase<Vendor>
    {
        Task<IEnumerable<Vendor>> GetAll();
        Task<Vendor> GetById(int id);
        Task<Vendor> GetByMobile(String id);
    }
    public class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public VendorRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Vendor>> GetAll()
        {
            var data = await this.Query().SelectAsync();

            return data;
        }
        public async Task<Vendor> GetById(int ids)
        {
            int id = ids;
            var data = await this.Query().SingleOrDefaultAsync(x => x.VendorId == id, System.Threading.CancellationToken.None);
            return data;
        }


        public async Task<Vendor> GetByMobile(string id)
        {

            var vendor = await this.Query().SingleOrDefaultAsync(c => c.ContactNo == id, CancellationToken.None);

            return vendor;
        }
    }
}
