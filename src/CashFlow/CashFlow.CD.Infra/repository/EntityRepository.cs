using CashFlow.CD.Domain.repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.CD.Infra.repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public EntityRepository(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<T>(collectionName);
        }

        public Task Create(T entity)
        {
            _collection.InsertOne(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> Read()
        {
            return await _collection.Find(e => true).ToListAsync();
        }

        public async Task<T> Read(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public Task Update(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            _collection.ReplaceOne(filter, entity);
            return Task.CompletedTask;
        }

        public Task Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            _collection.DeleteOne(filter);
            return Task.CompletedTask;
        }

        public async Task<T> FindOneAsync(FilterDefinition<T> filter)
        {
            IAsyncCursor<T> cursor = await _collection.FindAsync(filter);
            return await cursor.FirstOrDefaultAsync();
        }
    }
}
