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
    public interface IAtendenceRepository : IRepositoryBase<Atendence>
    {
        Task<IEnumerable<Atendence>> GetAll();
        Task<Atendence> GetById(int id);
    }
    public class AtendenceRepository : RepositoryBase<Atendence>, IAtendenceRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public AtendenceRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Atendence>> GetAll()
        {
            var Atendences = await this.Query().SelectAsync();

            return Atendences;
        }

        public async Task<Atendence> GetById(int id)
        {

            var Atendences = await this.Query().SingleOrDefaultAsync(c=>c.AtendenceId==id,CancellationToken.None);

            return Atendences;
        }
    }
}
