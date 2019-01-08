using Microsoft.Extensions.Configuration;
using mongoConsole.Models;
using mongoConsole.Repositories.Interfaces;
using MongoDB.Driver;

namespace mongoConsole.Repositories
{
    public class UserRepository: BaseRepository<Users>, IUserRepository {
        public UserRepository(IConfiguration config, IMongoClient mongoClient): base(config, mongoClient) { }                
    }
}