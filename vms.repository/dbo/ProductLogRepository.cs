using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.entity.viewModels;

namespace vms.repository.dbo
{
    public interface IProductLogRepository : IRepositoryBase<ProductLog>
    {
        Task<IEnumerable<ProductLog>> GetAll();
        Task<ProductLog> GetById(int id);
    }
    public class ProductLogRepository : RepositoryBase<ProductLog>, IProductLogRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public ProductLogRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public Task<IEnumerable<ProductLog>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProductLog> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
