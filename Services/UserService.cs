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

        public void AddUser(string userName, string firstName, string lastName)
        {
            var user = new Users{
                UserName = userName,
                Name = new NameModel {
                    FirstName = firstName,
                    LastName = lastName
                }
            };
            _userRepository.Create(user);
        }

        public List<Users> GetUsers() {
            return _userRepository.Query().ToList();
        }

        public void RemoveUser(string userName)
        {
            var filter = Builders<Users>.Filter.Eq("UserName", userName);
            var user = _userRepository.Query(filter).FirstOrDefault();

            if(user != null) {
                _userRepository.Remove(user);
            }
        }

        public void UpdateUser(string userName, string newUserName)
        {
            var filter = Builders<Users>.Filter.Eq("UserName", userName);
            var user = _userRepository.Query(filter).FirstOrDefault();

            if(user != null) {
                user.UserName = newUserName;
                _userRepository.Replace(user);
            }
        }
    }
}