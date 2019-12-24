
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface IProductPriceService : IServiceBase<ProductPrice>
    {
        Task<IEnumerable<ProductPrice>> GetAll();
        Task<ProductPrice> GetById(int id);
    }
    public class ProductPriceService : ServiceBase<ProductPrice>, IProductPriceService
    {
        public IProductPriceRepository _repository { get; }
        public ProductPriceService(IProductPriceRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ProductPrice>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ProductPrice> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
