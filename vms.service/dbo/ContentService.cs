using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;

namespace vms.service.dbo
{
    public interface IContentService : IServiceBase<Content>
    {
        Task<IEnumerable<Content>> GetAll();
        Task<Content> GetById(int id);
    }
    public class ContentService : ServiceBase<Content>, IContentService
    {
        public IContentRepository _repository { get; }
        public ContentService(IContentRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Content>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Content> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
