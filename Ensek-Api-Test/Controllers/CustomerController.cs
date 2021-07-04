using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Ensek_Api_Test.Data.Entities;
using Ensek_Api_Test.Data.Services;
using Ensek_Api_Test.DTOs;

namespace Ensek_Api_Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMeterAccountRepository _repository;
        private readonly ILogger<CustomerController> _logger;
        private CustomerDto _customerDto;

        public CustomerController(IMeterAccountRepository repository, ILogger<CustomerController> logger)
        {
            _repository = repository;
            _logger = logger;
            _customerDto = new CustomerDto();
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            try
            {
                var customers = _repository.GetAllCustomers();
                return Ok(_customerDto.FromCustomers(customers));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get customers: {ex}");
                return BadRequest("Failed to get customers");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            try
            {
                var customer = _repository.GetCustomer(id);
                var custDto = _customerDto.FromCustomer(customer);
                return Ok(custDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get customer with id of {id}: {ex}");
                return BadRequest("Failed to get customer");
            }
        }
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] CustomerDto customer)
        {
            try
            {
                var customerEntity = _customerDto.FromCustomerDto(customer);
                var saveCustomer = _repository.SaveCustomer(customerEntity);
                return Ok(_customerDto.FromCustomer(saveCustomer));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save customer: {ex}");
                return BadRequest($"Failed to save customer");
            }
        }
    }
}
