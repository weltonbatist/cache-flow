using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.CD.Domain.repository
{
    public interface IEntityRepository<T> where T : class
    {
        Task Create(T entity);
        Task<IEnumerable<T>> Read();
        Task<T> FindOneAsync(FilterDefinition<T> filter);
        Task<T> Read(string id);
        Task Update(string id, T entity);
        Task Delete(string id);

    }
}
