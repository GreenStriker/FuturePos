using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF.Trackable;

// Example: extending IRepository<TEntity> and/or ITrackableRepository<TEntity>, scope: application-wide across all IRepositoryX<TEntity>
namespace vms.repository
{
    public interface IRepositoryBase<TEntity> : ITrackableRepository<TEntity> where TEntity : class, ITrackable
    {
        // Example: adding synchronous Find, scope: application wide for all repositories
        TEntity Find(object[] keyValues, CancellationToken cancellationToken);
        System.Threading.Tasks.Task<System.Collections.Generic.List<T>> CreateQuery<T>(string sql, CancellationToken cancellationToken);


    }
    public class RepositoryBase<TEntity> : TrackableRepository<TEntity>, IRepositoryBase<TEntity> where TEntity : class, ITrackable
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context) : base(context)
        {
            this._context = context;
        }
        // Example: adding synchronous Find, scope: application-wide
        public TEntity Find(object[] keyValues, CancellationToken cancellationToken)
        {
            return this.Context.Find<TEntity>(keyValues) as TEntity;
        }
       
        public async System.Threading.Tasks.Task<System.Collections.Generic.List<T>> CreateQuery<T>(string sql, CancellationToken cancellationToken)
        {
            List<T> items = new List<T>();
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = sql;
                connection.Open();
                System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = Activator.CreateInstance<T>();
                        foreach (System.Reflection.PropertyInfo property in properties)
                        {
                            try
                            {
                                if (property.PropertyType.Name == "Nullable`1")
                                    property.SetValue(item, Convert.ChangeType(reader[property.Name], Nullable.GetUnderlyingType(property.PropertyType)), null);
                                else
                                    property.SetValue(item, Convert.ChangeType(reader[property.Name], property.PropertyType), null);
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        items.Add(item);
                    }
                }
            }

            return await System.Threading.Tasks.Task.FromResult(items);
        }

        

    }

     
}
