using System;
using System.Collections.Generic;
using Ensek_Api_Test.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Ensek_Api_Test.Data.DbContexts
{
    public class MeterAccountContext : DbContext
    {
        public MeterAccountContext(DbContextOptions<MeterAccountContext> options)
            : base(options)
        {
        }
        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
    }
}
