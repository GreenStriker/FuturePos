using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IStockService : IServiceBase<Stock>
    {
        Task<IEnumerable<Stock>> GetAll();
        Task<Stock> GetById(int id);
    }
    public class StockService : ServiceBase<Stock>, IStockService
    {
        public IStockRepository _repository { get; }
        public StockService(IStockRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Stock>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Stock> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
