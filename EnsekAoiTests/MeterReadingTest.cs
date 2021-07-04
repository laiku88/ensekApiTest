using System;
using Ensek_Api_Test.Data.Entities;
using Ensek_Api_Test.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnsekAoiTests
{
    [TestClass]
    public class MeterReadingTest
    {
        [TestMethod]
        public void MeterValueThrowsExceptionIfNegativeValuePassed()
        {
            try
            {
                var reading = -123;
                new MeterValue(reading).FormatMeterValue();
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }
        [TestMethod]
        public void MeterValueThrowsExceptionIfReadingIsTooLongPassed()
        {
            try
            {
                var reading = 999999;
                new MeterValue(reading).FormatMeterValue();
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }
        [TestMethod]
        public void MeterValueReturnsCorrectFormat()
        {
            var meterReading = new MeterValue(123);
            Assert.IsTrue(meterReading.FormatMeterValue() == "00123");
        }
        [TestMethod]
        public void InstantiateMeterValueDoesNotReturnNullReadingProperty()
        {
            var meterVal = new MeterValue(123);
            
            Assert.IsNotNull(meterVal.Reading);
        }
        [TestMethod]
        public void MeterValueFormatReadingReturnsFormattedString()
        {
            var meterVal = new MeterValue(123);
            var formattedVal = meterVal.FormatMeterValue();
            Assert.IsTrue(formattedVal == "00123");
        }
        [TestMethod]
        public void FromMeterReadingDtoReturnsMeterReadingValueInCorrectFormat()
        {
            var meterReadingDto = new MeterReadingDto
            {
                MeterReadingValue = new MeterValue(123),
                MeterReadingDateTime = DateTime.Now,
                
            };
            var meterReading = meterReadingDto.FromMeterReadingDto(meterReadingDto);
            Assert.IsTrue(meterReading.MeterReadingValue == "00123");
        }
        [TestMethod]
        public void FromMeterReadingReturnsMeterReadingValueAsInt()
        {
            var mr = new MeterReading
            {
                MeterReadingValue = "00123",
                MeterReadingDateTime = DateTime.Now,
            
            };
            var meterReading = new MeterReadingDto().FromMeterReading(mr);
            Assert.IsTrue(meterReading.MeterReadingValue.Reading == 123);
        }
    }
}
