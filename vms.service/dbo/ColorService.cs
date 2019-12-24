
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IColorService : IServiceBase<Color>
    {
        Task<IEnumerable<Color>> GetAll();
        Task<Color> GetById(int id);
    }
    public class ColorService : ServiceBase<Color>, IColorService
    {
        public IColorRepository _repository { get; }
        public ColorService(IColorRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Color>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Color> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
