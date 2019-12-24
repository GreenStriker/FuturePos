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
    public interface IExpenceTypeRepository : IRepositoryBase<ExpenceType>
    {
        Task<IEnumerable<ExpenceType>> GetAll();
        Task<ExpenceType> GetById(int id);
    }
    public class ExpenceTypeRepository : RepositoryBase<ExpenceType>, IExpenceTypeRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public ExpenceTypeRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<ExpenceType>> GetAll()
        {
            var ExpenceTypes = await this.Query().SelectAsync();

            return ExpenceTypes;
        }

        public async Task<ExpenceType> GetById(int id)
        {

            var ExpenceTypes = await this.Query().SingleOrDefaultAsync(c=>c.ExpenceTypeId==id,CancellationToken.None);

            return ExpenceTypes;
        }
    }
}
