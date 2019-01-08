using System.Collections.Generic;
using mongoConsole.Models;
using mongoConsole.Repositories.Interfaces;
using MongoDB.Driver;
using mongoConsole.Services.Repositories;

namespace mongoConsole.Services
{
    public class UserService: IUserService {

        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<Users> GetUsers() {
            return _userRepository.Query().ToList();
        }
    }
}