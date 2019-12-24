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
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<Customer> GetByMobile(String id);
    }
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public CustomerRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Customer>> GetAll()
        {
            var Customers = await this.Query().SelectAsync();

            return Customers;
        }

        public async Task<Customer> GetById(int id)
        {

            var Customers = await this.Query().SingleOrDefaultAsync(c=>c.CustomerId==id,CancellationToken.None);

            return Customers;
        }




        public async Task<Customer> GetByMobile(string id)
        {

            var Customers = await this.Query().SingleOrDefaultAsync(c => c.Mobile == id, CancellationToken.None);

            return Customers;
        }
    }
}
