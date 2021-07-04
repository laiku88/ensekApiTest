using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ensek_Api_Test.Data.Entities;

namespace Ensek_Api_Test.Data.Services
{
    public interface IMeterAccountRepository
    {
        public IEnumerable<Customer> GetAllCustomers();
        public Customer GetCustomer(int id);
        public MeterReading GetMeterReading(MeterReading meterReading);
        public MeterReading SaveMeterReading(MeterReading meterReading);
        public bool DeleteAllMeterReadings();
        public bool DeleteMeterReadingById(int id);
        public IEnumerable<MeterReading> GetMeterReadingsByCustomerId(int customerId);
        public IEnumerable<MeterReading> GetAllMeterReadings();
        public Customer SaveCustomer(Customer customerEntity);
    }
}
