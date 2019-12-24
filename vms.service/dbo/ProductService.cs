using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface IProductService : IServiceBase<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
    }
    public class ProductService : ServiceBase<Product>, IProductService
    {
        public IProductRepository _repository { get; }
        public ProductService(IProductRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Product> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
