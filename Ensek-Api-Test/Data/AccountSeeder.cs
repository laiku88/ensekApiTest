using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ensek_Api_Test.Data.DbContexts;
using Ensek_Api_Test.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ensek_Api_Test.Data
{
    public class AccountSeeder
    {
        private readonly MeterAccountContext _ctx;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<AccountSeeder> _logger;

        public AccountSeeder(MeterAccountContext ctx, IWebHostEnvironment env, ILogger<AccountSeeder> logger)
        {
            _ctx = ctx;
            _env = env;
            _logger = logger;
        }

        public void Seed()
        {
            try
            {
                _ctx.Database.EnsureCreated();
                if (_ctx.Customers.Any()) return;
                //seed DB
                var filePath = Path.Combine(_env.ContentRootPath, "Data/Seed_Test_Accounts.csv");
                var customersSeed = new HelperCsv.CsvHelpers().ReadCustomerCsvFile(filePath);
                _ctx.Database.OpenConnection();
                _ctx.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customers ON");
                _ctx.Customers.AddRange(customersSeed);
                _ctx.SaveChanges();
                _ctx.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customers OFF");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't seed the DB! Error:{ex.Message}");
            }
            finally
            {
                _ctx.Database.CloseConnection();
            }

           
        }
    }
}
