using System;
using System.IO;
using mongoConsole.Repositories;
using mongoConsole.Repositories.Interfaces;
using mongoConsole.Services.Interfaces;
using mongoConsole.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using MongoDB.Driver;

namespace mongoConsole {
    class Program 
    {

        static void Main (string[] args) {
            var command = args;
            var serviceProvider = GetProviderWithDependencyInjection();            
            var userService = serviceProvider.GetService<IUserService> ();
            var commandHandler = new CommandHandler(userService);

            while(!command.IsValidCommand()) {
                Console.WriteLine("Please enter a valid command. type help for more info.");
                command = Console.ReadLine().Split(' ');
            }            
                       
            commandHandler.HandleCommand(command);

            Console.WriteLine ("All done");
        }

        
        static IServiceProvider GetProviderWithDependencyInjection () 
        {            
            var services = new ServiceCollection ();
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

        static IConfigurationRoot GetConfigurationInstance () 
        {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true);

            return builder.Build ();
        }

        static MongoClient GetMongoClient(IConfiguration config) 
        {
            return new MongoClient(config.GetConnectionString("playGroundDB"));
        }
    }
}