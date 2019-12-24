
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IVatService : IServiceBase<Vat>
    {
        Task<IEnumerable<Vat>> GetAll();
        Task<Vat> GetById(int id);
    }
    public class VatService : ServiceBase<Vat>, IVatService
    {
        public IVatRepository _repository { get; }
        public VatService(IVatRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Vat>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Vat> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
