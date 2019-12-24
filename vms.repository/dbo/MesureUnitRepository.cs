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
    public interface IMesureUnitRepository : IRepositoryBase<MeasureUnit>
    {
        Task<IEnumerable<MeasureUnit>> GetAll();
        Task<MeasureUnit> GetById(int id);
    }
    public class MesureUnitRepository : RepositoryBase<MeasureUnit>, IMesureUnitRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public MesureUnitRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<MeasureUnit>> GetAll()
        {
            var vats = await this.Query().SelectAsync();

            return vats;
        }

        public async Task<MeasureUnit> GetById(int id)
        {

            var vats = await this.Query().SingleOrDefaultAsync(c=>c.MunitId==id,CancellationToken.None);

            return vats;
        }
    }
}
