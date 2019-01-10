using mongoConsole.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace mongoConsole.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseMongoModel
    {
        T Get(string id);
        IFindFluent<T, T> Query(FilterDefinition<T> filter = null);
        T Create(T record);
        void Replace(T record);
        void Remove(T record);
        void Remove(ObjectId id);
    }
}

