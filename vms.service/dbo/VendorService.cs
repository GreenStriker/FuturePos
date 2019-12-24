using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IVendorService : IServiceBase<Vendor>
    {
        Task<IEnumerable<Vendor>> GetAll();
        Task<Vendor> GetById(int id);
        Task<Vendor> GetByMobile(string id);
    }
    public class VendorService : ServiceBase<Vendor>, IVendorService
    {
        public IVendorRepository _repository { get; }
        public VendorService(IVendorRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Vendor>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Vendor> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Vendor> GetByMobile(string id)
        {
            return await _repository.GetByMobile(id);
        }
    }
}
