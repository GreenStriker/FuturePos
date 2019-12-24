
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface ICustomerService : IServiceBase<Customer>
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<Customer> GetByMobile(string id);
    }
    public class CustomerService : ServiceBase<Customer>, ICustomerService
    {
        public ICustomerRepository _repository { get; }
        public CustomerService(ICustomerRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _repository.GetById(id);
        }


        public async Task<Customer> GetByMobile(string id)
        {
            return await _repository.GetByMobile(id);
        }
    }
}
