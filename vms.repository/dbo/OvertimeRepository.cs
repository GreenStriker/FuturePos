using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.entity.viewModels;

namespace vms.repository.dbo
{

    public interface IOvertimeRepository : IRepositoryBase<Overtime>
    {
        Task<IEnumerable<Overtime>> GetUsers(int p_orgId);
        Task<Overtime> GetUser(int id);
    }
    public class OvertimeRepository : RepositoryBase<Overtime>, IOvertimeRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public OvertimeRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Overtime>> GetUsers(int p_orgId)
        {
            var users = await this.Query().SelectAsync();
          
            return users;
        }
        public async Task<Overtime> GetUser(int ids)
        {
            int id = ids;
            var user = await this.Query()
             
                .SingleOrDefaultAsync(x => x.OvertimeId == id, System.Threading.CancellationToken.None);
           

            return user;
        }
    }


     
}
