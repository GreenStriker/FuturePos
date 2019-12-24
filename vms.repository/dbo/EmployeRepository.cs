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
    public interface IEmployeRepository : IRepositoryBase<Employe>
    {
        Task<IEnumerable<Employe>> GetAll();
        Task<Employe> GetById(int id);
    }
    public class EmployeRepository : RepositoryBase<Employe>, IEmployeRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public EmployeRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Employe>> GetAll()
        {
            var Employes = await this.Query().SelectAsync();

            return Employes;
        }

        public async Task<Employe> GetById(int id)
        {

            var Employes = await this.Query().SingleOrDefaultAsync(c=>c.EmployeId==id,CancellationToken.None);

            return Employes;
        }
    }
}
