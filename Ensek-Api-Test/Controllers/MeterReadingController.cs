using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Ensek_Api_Test.BL;
using Ensek_Api_Test.Data.Entities;
using Ensek_Api_Test.Data.Services;
using Ensek_Api_Test.DTOs;
using Ensek_Api_Test.HelperCsv;
using Microsoft.Extensions.Logging;

namespace Ensek_Api_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterAccountRepository _repository;
        private readonly ILogger<MeterReadingController> _logger;

        public MeterReadingController(IMeterAccountRepository repository, ILogger<MeterReadingController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Route("meter-readings-by-customerId/{customerId}")]
        public ActionResult<MeterReading> Get(int customerId)
        {
            try
            {
                return Ok(_repository.GetMeterReadingsByCustomerId(customerId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get customers: {ex}");
                return BadRequest("Failed to get customers");
            }
        }
        [HttpGet]
        [Route("meter-readings-all")]
        public ActionResult<MeterReading> Get()
        {
            try
            {
                return Ok(_repository.GetAllMeterReadings());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get customers: {ex}");
                return BadRequest("Failed to get customers");
            }
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        public IActionResult Post(IFormFile file)
        {
            try
            {
                var readings = new CsvHelpers().ReadMeterReadingCsvFile(file);
                var result = 
                    new ProcessMeterReadings(_repository, new MeterReadRulesEngine(_repository), new MeterReadingDto()).TryUpLoadMeterReadings(readings);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to upload meterReading file: {ex}");
                return BadRequest("Failed to upload meterReading file");
            }
        }

        [HttpDelete]
        [Route("meter-reading-delete")]
        public IActionResult Delete()
        {
            try
            {
                var deleted = _repository.DeleteAllMeterReadings();
                if(deleted)return Ok();
                throw new Exception("Failed to delete all meter readings");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete all meter readings: {ex}");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("meter-reading-delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleted = _repository.DeleteMeterReadingById(id);
                if(deleted)return Ok();
                throw new Exception($"Failed to delete meter reading with Id of {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete meter reading with id of {id}: {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}
