
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface IExpenceTypeService : IServiceBase<ExpenceType>
    {
        Task<IEnumerable<ExpenceType>> GetAll();
        Task<ExpenceType> GetById(int id);
    }
    public class ExpenceTypeService : ServiceBase<ExpenceType>, IExpenceTypeService
    {
        public IExpenceTypeRepository _repository { get; }
        public ExpenceTypeService(IExpenceTypeRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ExpenceType>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ExpenceType> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
