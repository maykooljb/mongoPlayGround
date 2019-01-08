using mongoConsole.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using mongoConsole.Repositories.Interfaces;

namespace mongoConsole.Repositories
{
    public abstract class BaseRepository<T>: IBaseRepository<T> where T : BaseMongoModel {
        private readonly IMongoCollection<T> _collection;

        public BaseRepository(IConfiguration config, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(config["dbName"]);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public T Get(string id) {
            var oId = new ObjectId(id);
            return _collection.Find<T>(r => r.Id == oId).FirstOrDefault();
        }

        public IFindFluent<T, T> Query() {
            return _collection.Find(r => true);
        }

        public T Create(T record) {
            _collection.InsertOne(record);
            return record;
        }

        public void Replace(string id, T record) {
            var docId = new ObjectId(id);
            _collection.ReplaceOne(r => record.Id == docId, record);            
        }

        public void Remove(T record)
        {
            _collection.DeleteOne(r => r.Id == record.Id);
        }

        public void Remove(ObjectId id)
        {
            _collection.DeleteOne(r => r.Id == id);
        }
    }
}

