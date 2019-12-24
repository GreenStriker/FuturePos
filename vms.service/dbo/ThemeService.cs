
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IThemeService : IServiceBase<Theme>
    {
        Task<IEnumerable<Theme>> GetAll();
        Task<Theme> GetById(int id);
    }
    public class ThemeService : ServiceBase<Theme>, IThemeService
    {
        public IThemeRepository _repository { get; }
        public ThemeService(IThemeRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Theme>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Theme> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
