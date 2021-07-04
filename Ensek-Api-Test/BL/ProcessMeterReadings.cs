using System;
using System.Collections.Generic;
using System.Linq;
using Ensek_Api_Test.Data.Services;
using Ensek_Api_Test.DTOs;

namespace Ensek_Api_Test.BL
{
    public class ProcessMeterReadings
    {
        private readonly IMeterAccountRepository _repository;
        private readonly MeterReadRulesEngine _rulesEngine;
        private readonly MeterReadingDto _mrDto;

        public ProcessMeterReadings(IMeterAccountRepository repository, MeterReadRulesEngine rulesEngine, MeterReadingDto mrDto)
        {
            _repository = repository;
            _rulesEngine = rulesEngine;
            _mrDto = mrDto;
        }

        public Dictionary<string, IEnumerable<object>> TryUpLoadMeterReadings(IEnumerable<dynamic> readings)
        {
            var success = new List<MeterReadingDto>();
            var fail = new List<object>();
            var duplicates = new List<MeterReadingDto>();
            foreach (var r in readings)
            {
                if (PassesAllRules(r))
                {
                    var meterReadingDto = MapFileDataToDto();
                    // check if meterReading already exists in Success
                    if (success.Any(x => x.MeterReadingValue.Reading 
                                         == meterReadingDto.MeterReadingValue.Reading
                                         && x.CustomerId == meterReadingDto.CustomerId))
                    {
                        duplicates.Add(meterReadingDto);
                    }
                    else
                    {
                        var mrEntity = _mrDto.FromMeterReadingDto(meterReadingDto);
                        var savedInDb = _repository.SaveMeterReading(mrEntity);
                        if (savedInDb !=null)
                        {
                            success.Add(_mrDto.FromMeterReading(savedInDb));
                        }
                        else
                        {
                            fail.Add(meterReadingDto);
                        }
                    }
                }
                else
                {
                    fail.Add(new
                    {
                        MeterReadingValue = r.MeterReadValue,
                        CustomerId =r.AccountId, 
                        MeterReadingDateTime =r.MeterReadingDateTime
                    });
                }
            }
            var successFailResults = new Dictionary<string, IEnumerable<object>>
            {
                {"success", success}, {"fail", fail}, {"duplicates", duplicates}
            };
            return successFailResults;
        }

        private bool PassesAllRules(dynamic reading)
        {
            var accountId = reading.AccountId;
            var date = reading.MeterReadingDateTime;
            var meterReadVal = reading.MeterReadValue;
            return (_rulesEngine.IsValidAccountId(accountId) 
                    && _rulesEngine.IsValidMeterDate(date)
                    && _rulesEngine.IsValidMeterReading(meterReadVal));
        }

        private MeterReadingDto MapFileDataToDto()
        {
            var mrDto = new MeterReadingDto
            {
                MeterReadingValue = _rulesEngine.MeterReadValue,
                CustomerId = _rulesEngine.AccountId,
                MeterReadingDateTime = _rulesEngine.MeterReadDateTime
            };
            return mrDto;
        }
    }
}
