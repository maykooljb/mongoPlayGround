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
            var serviceProvider = getProviderWithDependencyInjection (services);

            var userService = serviceProvider.GetService<IUserService> ();            
            Console.WriteLine ("Printing List of Records in Users Colletion");

            var usersList = userService.GetUsers ();
            foreach (var user in usersList) {
                Console.WriteLine ($"UserName: {user.UserName}, First Name: {user.Name.FirstName}, Last Name: {user.Name.LastName}.");
            }

            Console.WriteLine ("All done");
        }

        
        //Uses custom DI container (struct map) to automatically map the interfaces
        static IServiceProvider getProviderWithDependencyInjection (ServiceCollection services) {            
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