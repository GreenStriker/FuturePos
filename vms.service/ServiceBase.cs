using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Services;
using URF.Core.Services;
using vms.repository;

namespace vms.service
{
    public interface IServiceBase<TEntity> : IService<TEntity>, IRepositoryBase<TEntity> where TEntity : class, ITrackable
    {
        Task<List<T>> CreateQuery<T>(string sql, CancellationToken cancellationToken);
        TEntity Find(object[] keyValues, CancellationToken cancellationToken);

    }
    public class ServiceBase<TEntity> : Service<TEntity>, IServiceBase<TEntity> where TEntity : class, ITrackable
    {
        private readonly IRepositoryBase<TEntity> repository;

        protected ServiceBase(IRepositoryBase<TEntity> repository) : base(repository)
        {
            this.repository = repository;
        }

        public async Task<List<T>> CreateQuery<T>(string sql, CancellationToken cancellationToken)
        {
            return await this.repository.CreateQuery<T>(sql, cancellationToken);
        }

        public TEntity Find(object[] keyValues, CancellationToken cancellationToken)
        {
            return this.repository.Find(keyValues, cancellationToken);
        }
    }
}
