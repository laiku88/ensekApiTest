using System;
using System.Collections.Generic;
using System.Linq;
using Ensek_Api_Test.Data.DbContexts;
using Ensek_Api_Test.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ensek_Api_Test.Data.Services
{
    public class MeterAccountRepository : IMeterAccountRepository
    {
        private readonly MeterAccountContext _context;
        private readonly ILogger<MeterAccountRepository> _logger;

        public MeterAccountRepository(MeterAccountContext context, ILogger<MeterAccountRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            try
            {
                var query = _context.Customers.Include(x => x.MeterReadings);
                return query.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred getting all Customers: {ex}");
                return null;
            }
        }
        public Customer GetCustomer(int id)
        {
            try
            {
                var customer = _context.Customers.Include(x => x.MeterReadings)
                    .FirstOrDefault(y => y.Id == id);
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred getting customer with id {id}: {ex}");
                return null;
            }
        }

        public  MeterReading GetMeterReading(MeterReading meterReading)
        {
            var query = _context.MeterReadings.FirstOrDefault(x =>
                x.MeterReadingValue == meterReading.MeterReadingValue
                && x.CustomerId == meterReading.CustomerId && x.MeterReadingDateTime == meterReading.MeterReadingDateTime);
            return query;
        }

        public MeterReading SaveMeterReading(MeterReading meterReading)
        {
            try
            {
                var existingReading = GetMeterReading(meterReading);
                if (existingReading != null) return null;
                _context.MeterReadings.Add(meterReading);
                var result = _context.SaveChanges();
               if(result > 0) return meterReading;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred adding the meter reading for customer with id of {meterReading.CustomerId}: {ex}");
                throw ex;
            }
            return null;
        }

        public bool DeleteAllMeterReadings()
        {
            var meterReadings = _context.MeterReadings;
            if (!meterReadings.Any()) return true;
            _context.MeterReadings.RemoveRange(meterReadings);
            var result = _context.SaveChanges();
            return result > 0;
        }
        public bool DeleteMeterReadingById(int id)
        {
            var meterReading = _context.MeterReadings.FirstOrDefault(x=>x.Id ==id);
            if (meterReading == null) throw new Exception($"No meter reading with id of {id}");
            _context.MeterReadings.Remove(meterReading);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public IEnumerable<MeterReading> GetMeterReadingsByCustomerId(int customerId)
        {
            var query = _context.MeterReadings.Where(x => x.CustomerId == customerId);
            return query.ToList();
        }

        public IEnumerable<MeterReading> GetAllMeterReadings()
        {
            return _context.MeterReadings.ToList();
        }

        public Customer SaveCustomer(Customer customerEntity)
        {
            _context.Customers.Add(customerEntity);
            var result = _context.SaveChanges();
            if (result > 0)
                return customerEntity;
            throw new Exception($"customer {customerEntity.FirstName} {customerEntity.LastName} was not saved");
        }
    }
}
