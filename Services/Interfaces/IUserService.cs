using System.Collections.Generic;
using mongoConsole.Models;

namespace mongoConsole.Services.Interfaces
{
    public interface IUserService
    {
        List<Users> GetUsers();
        void AddUser(string userName, string firstName, string lastName);
        void RemoveUser(string userName);
        void UpdateUser(string userName, string newUserName);
    }
}