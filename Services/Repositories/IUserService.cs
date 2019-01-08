using System.Collections.Generic;
using mongoConsole.Models;

namespace mongoConsole.Services.Repositories
{
    public interface IUserService
    {
        List<Users> GetUsers();
    }
}