using System;
using Ensek_Api_Test.Data.Services;
using Ensek_Api_Test.DTOs;

namespace Ensek_Api_Test.BL
{
    public class MeterReadRulesEngine
    {
        private readonly IMeterAccountRepository _repository;
        public int AccountId { get; private set; }
        public MeterValue MeterReadValue { get; private set; }
        public DateTime MeterReadDateTime { get; private set; }

        public MeterReadRulesEngine(IMeterAccountRepository repository)
        {
            _repository = repository;
        }
        public bool IsValidMeterReading(string meterReadVal)
        {
            if (!int.TryParse(meterReadVal, out var validMeterRead)) return false;
            if (meterReadVal.Length > 5) return false;
            if (validMeterRead < 0) return false;
            MeterReadValue = new MeterValue(validMeterRead);
            return true;
        }

        public bool IsValidMeterDate(string date)
        {
            if (!DateTime.TryParse(date, out var validDate)) return false;
            MeterReadDateTime = validDate;
            return true;
        }
        public bool IsValidAccountId(string accountId)
        {
            if (!int.TryParse(accountId, out var validAccountId)) return false;
            if (_repository.GetCustomer(validAccountId) == null) return false;
            AccountId = validAccountId;
            return true;
        }
    }
}
