using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.RC.Infra.repository
{
    public class EntityRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public EntityRepository(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<T>(collectionName);
        }

        public void Create(T entity)
        {
            _collection.InsertOne(entity);
        }

        public IEnumerable<T> Read()
        {
            return _collection.Find(e => true).ToList();
        }

        public T Read(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return _collection.Find(filter).FirstOrDefault();
        }

        public void Update(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            _collection.ReplaceOne(filter, entity);
        }

        public void Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            _collection.DeleteOne(filter);
        }
    }
}
