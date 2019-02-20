using System;
using System.Collections.Generic;
using mongoConsole.Models;
using mongoConsole.Repositories.Interfaces;
using MongoDB.Driver;
using mongoConsole.Services.Interfaces;

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
            if(userName == null) throw new ArgumentNullException(nameof(userName));
            if(firstName == null) throw new ArgumentNullException(nameof(firstName));
            if(lastName == null) throw new ArgumentNullException(nameof(lastName));
            
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
            if(userName == null) throw new ArgumentNullException(nameof(userName));
                        
            var filter = Builders<Users>.Filter.Eq("UserName", userName);
            var user = _userRepository.Query(filter).FirstOrDefault();

            if(user != null) {
                _userRepository.Remove(user);
            }
        }

        public void UpdateUser(string userName, string newUserName)
        {
            if(userName == null) throw new ArgumentNullException(nameof(userName));
            if(newUserName == null) throw new ArgumentNullException(nameof(newUserName));
            
            var filter = Builders<Users>.Filter.Eq("UserName", userName);
            var user = _userRepository.Query(filter).FirstOrDefault();

            if(user != null) {
                user.UserName = newUserName;
                _userRepository.Replace(user);
            }
        }
    }
}