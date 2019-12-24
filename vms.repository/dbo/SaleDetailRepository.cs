using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.entity.viewModels;
using vms.repository;

namespace vms.repository.dbo
{
    public interface ISaleDetailRepository : IRepositoryBase<SalesDetail>
    {
        Task<IEnumerable<SalesDetail>> GetAll();
        Task<SalesDetail> GetById(int id);
    }
    public class SaleDetailRepository : RepositoryBase<SalesDetail>, ISaleDetailRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public SaleDetailRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<SalesDetail>> GetAll()
        {
            var data = await this.Query().SelectAsync();

            return data;
        }
        public async Task<SalesDetail> GetById(int ids)
        {
            int id = ids;
            var data = await this.Query().SingleOrDefaultAsync(x => x.SaleId == id, System.Threading.CancellationToken.None);
            return data;
        }
    }
}
