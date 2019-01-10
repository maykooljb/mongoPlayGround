using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mongoConsole.Models {
    public abstract class BaseMongoModel {
        
        public ObjectId Id {get; set;}
    }
}