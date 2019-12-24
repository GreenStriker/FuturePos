using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IContentTypeService : IServiceBase<Contenttype>
    {
        Task<IEnumerable<Contenttype>> GetAll();
        Task<Contenttype> GetById(int id);
    }
    public class ContentTypeService : ServiceBase<Contenttype>, IContentTypeService
    {
        public IContentTypeRepository _repository { get; }
        public ContentTypeService(IContentTypeRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Contenttype>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Contenttype> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
