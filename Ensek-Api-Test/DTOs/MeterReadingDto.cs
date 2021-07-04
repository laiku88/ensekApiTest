using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ensek_Api_Test.Data.Entities;

namespace Ensek_Api_Test.DTOs
{
    public class MeterReadingDto
    {
        [Required]
        public int MeterReadingId { get;  protected set; }
        [Required]
        public DateTime MeterReadingDateTime { get; set; }

        [Required] 
        public MeterValue MeterReadingValue { get; set; }
        [Required] 
        public int CustomerId { get; set; }

        public CustomerDto Customer { get; set; }

        public MeterReading FromMeterReadingDto(MeterReadingDto meterReadingDto)
        {
            var customer = meterReadingDto.Customer == null
                ? null
                : new Customer
                {
                    FirstName = meterReadingDto.Customer.FirstName,
                    Id = meterReadingDto.Customer.AccountId,
                    LastName = meterReadingDto.Customer.LastName
                };
            return new MeterReading
            {
                Customer = customer,
                CustomerId = meterReadingDto.CustomerId,
                Id = meterReadingDto.MeterReadingId, 
                MeterReadingDateTime = meterReadingDto.MeterReadingDateTime, 
                MeterReadingValue = meterReadingDto.MeterReadingValue.FormatMeterValue(),
            };
        }

        public IEnumerable<MeterReading> FromMeterReadingDtos(IEnumerable<MeterReadingDto> meterReadingDtos)
        {
            var meterReadings = meterReadingDtos.Select(FromMeterReadingDto).ToList();
            return meterReadings;
        }


        public MeterReadingDto FromMeterReading(MeterReading meterReading)
        {
            var customer = meterReading.Customer == null ? null : new CustomerDto().FromCustomer(meterReading.Customer);
            return new MeterReadingDto
            {
                MeterReadingId= meterReading.Id, 
                MeterReadingDateTime = meterReading.MeterReadingDateTime, 
                MeterReadingValue = new MeterValue(Convert.ToInt32(meterReading.MeterReadingValue)),
                CustomerId = meterReading.Customer?.Id ?? 0,
                Customer = customer
            };
        }
    }
    public struct MeterValue
    {

        public MeterValue(int reading)
        {
            Reading = reading;
        }

        public int Reading { get; set; }
        public string FormatMeterValue()
        {
            if (Reading < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Reading),"Value cannot be less than 0");
            }
            if (Reading.ToString().Length > 5)
            {
                throw new ArgumentException("Value cannot have more than 5 digits");
            }
            return Reading.ToString("00000");
        }
    }
}
