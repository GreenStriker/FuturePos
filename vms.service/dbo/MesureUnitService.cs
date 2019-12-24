
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface IMesureUnitService : IServiceBase<MeasureUnit>
    {
        Task<IEnumerable<MeasureUnit>> GetAll();
        Task<MeasureUnit> GetById(int id);
    }
    public class MesureUnitService : ServiceBase<MeasureUnit>, IMesureUnitService
    {
        public IMesureUnitRepository _repository { get; }
        public MesureUnitService(IMesureUnitRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<MeasureUnit>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<MeasureUnit> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
