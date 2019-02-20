using System;
using mongoConsole.Services.Interfaces;

namespace mongoConsole.Utils 
{
    public class CommandHandler
    {        
        private readonly IUserService _userService;        
     
        public CommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        public void HandleCommand(string[] commandArguments)
        {
            var command = Enum.Parse(typeof(ValidCommands), commandArguments[0],true);
            var userName = commandArguments.GetOrDefault(1);
            switch(command) 
            {
                case ValidCommands.Show:
                    PrintUsersList();
                break;

                case ValidCommands.Add:
                    _userService.AddUser(userName, commandArguments[2], commandArguments[3]);
                    Console.WriteLine ($"New User Added: UserName: {userName}, First Name: {commandArguments[2]}, Last Name: {commandArguments[3]}.");
                    PrintUsersList();
                break;

                case ValidCommands.Remove:
                    _userService.RemoveUser(userName);
                    Console.WriteLine($"Removed user with Name {userName}");
                    PrintUsersList();
                break;

                case ValidCommands.Update:
                    _userService.UpdateUser(userName, commandArguments[2]);
                    Console.WriteLine($"Update user name from {userName} to {commandArguments[2]}");
                    PrintUsersList();
                break;
            }
        }

        public void PrintUsersList() 
        {
            Console.WriteLine("Records in collection Users:");
            var usersList = _userService.GetUsers ();
            foreach (var user in usersList) {
                Console.WriteLine ($"UserName: {user.UserName}, First Name: {user.Name.FirstName}, Last Name: {user.Name.LastName}.");
            }
        }

    }
}