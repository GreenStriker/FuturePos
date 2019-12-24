using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IProductLogService : IServiceBase<ProductLog>
    {
        Task<IEnumerable<ProductLog>> GetAll();
        Task<ProductLog> GetById(int id);
    }

    public class ProductLogService : ServiceBase<ProductLog>, IProductLogService
    {
        public IProductLogRepository _repository { get; }
        public ProductLogService(IProductLogRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<ProductLog>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProductLog> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
