using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Ensek_Api_Test.Data;
using Ensek_Api_Test.Data.DbContexts;

namespace Ensek_Api_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            //if (args.Length == 1 && args[0].ToLower() == "/seed")
            //{
            //    RunSeeding(host);
            //}
            //else
            //{
            //    // run the web app
            //    host.Run();
            //}
            RunSeeding(host);
            host.Run();
        }

        private static void RunSeeding(IHost host)
        {
            // migrate the database.  Best practice = in Main, using service scope
            using var scope = host.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<MeterAccountContext>();
                   
                // for demo purposes, delete the database & migrate on startup so 
                // we can start with a clean slate
                context.Database.EnsureDeleted();
                context.Database.Migrate();
                var seeder = scope.ServiceProvider.GetService<AccountSeeder>();
                seeder.Seed();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
