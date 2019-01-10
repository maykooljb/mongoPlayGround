using System;
using System.IO;
using mongoConsole.Repositories;
using mongoConsole.Repositories.Interfaces;
using mongoConsole.Services.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using MongoDB.Driver;

namespace mongoConsole {
    class Program {
        static void Main (string[] args) {
            var services = new ServiceCollection ();
            var serviceProvider = GetProviderWithDependencyInjection (services);

            var userService = serviceProvider.GetService<IUserService> ();            
            
            if(args[0] == "show") {                
                PrintUsersList(userService);

            } else if(args[0] == "add") {
                userService.AddUser(args[1], args[2], args[3]);
                Console.WriteLine ($"New User Added: UserName: {args[1]}, First Name: {args[2]}, Last Name: {args[3]}.");
                PrintUsersList(userService);

            } else if(args[0] == "remove") {
                userService.RemoveUser(args[1]);
                Console.WriteLine($"Removed user with Name {args[1]}");
                PrintUsersList(userService);
                
                PrintUsersList(userService);
            } else if(args[0] == "update") {
                userService.UpdateUser(args[1], args[2]);
                Console.WriteLine($"Update user name from {args[1]} to {args[2]}");
                PrintUsersList(userService);

            }
            
            Console.WriteLine ("All done");
        }

        static void PrintUsersList(IUserService userService) {
            Console.WriteLine("Records in collection Users:");
            var usersList = userService.GetUsers ();
            foreach (var user in usersList) {
                Console.WriteLine ($"UserName: {user.UserName}, First Name: {user.Name.FirstName}, Last Name: {user.Name.LastName}.");
            }
        }
        
        //Uses custom DI container (struct map) to automatically map the interfaces
        static IServiceProvider GetProviderWithDependencyInjection (ServiceCollection services) {            
            var container = new Container ();
            container.Configure (config => {
                config.Scan (scan => {
                    scan.AssemblyContainingType (typeof (Program));
                    scan.WithDefaultConventions ();
                });

                var configuration = GetConfigurationInstance();  
                var mongoClient = GetMongoClient(configuration); 
                
                config.For<IConfiguration> ().Use(configuration).Singleton ();                
                config.For<IMongoClient>().Use(mongoClient).Singleton(); //BestPractice: Always use a singleton mongoClient
                
                config.Populate (services);
            });
            
            return container.GetInstance<IServiceProvider> ();
        }

        static IConfigurationRoot GetConfigurationInstance () {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true);

            return builder.Build ();
        }

        static MongoClient GetMongoClient(IConfiguration config) {
            return new MongoClient(config.GetConnectionString("playGroundDB"));
        }
    }
}