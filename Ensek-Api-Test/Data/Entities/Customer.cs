using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ensek_Api_Test.DTOs;

namespace Ensek_Api_Test.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get;set; }
        public string LastName { get; set; }
        public ICollection<MeterReading> MeterReadings { get; set; }
    }
}
