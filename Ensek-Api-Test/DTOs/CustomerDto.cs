using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ensek_Api_Test.Data.Entities;

namespace Ensek_Api_Test.DTOs
{
    public class CustomerDto
    {
        public int AccountId { get; private set; }
        public string FirstName { get;set; }
        public string LastName { get; set; }
        public IEnumerable<MeterReadingDto> MeterReadings { get; set; }

        public CustomerDto FromCustomer(Customer customer)
        {
            return new CustomerDto()
            {
                FirstName = customer.FirstName, 
                LastName =  customer.LastName,
                AccountId = customer.Id
            };
        }

        public Customer FromCustomerDto(CustomerDto customer)
        {
            return new Customer()
            {
                FirstName = customer.FirstName, 
                LastName =  customer.LastName,
                Id = customer.AccountId
            };
        }
        public IEnumerable<Customer> FromCustomerDtos(IEnumerable<CustomerDto> customerDtos)
        {
            return customerDtos.Select(FromCustomerDto).ToList();
        }
        public IEnumerable<CustomerDto> FromCustomers(IEnumerable<Customer> customers)
        {
            return customers.Select(FromCustomer).ToList();
        }
    }
}
