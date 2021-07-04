using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Ensek_Api_Test.Data.Entities;
using Ensek_Api_Test.DTOs;
using Microsoft.AspNetCore.Http;

namespace Ensek_Api_Test.HelperCsv
{
    public class CsvHelpers
    {
        public IEnumerable<Customer> ReadCustomerCsvFile(string location) {  
            try
            {
                using var reader = new StreamReader(location, Encoding.Default);
                using var csv = new CsvReader(reader,CultureInfo.CurrentCulture);
                csv.Context.RegisterClassMap<CustomerMap>();
                var records = csv.GetRecords <Customer>();  
                return records.ToList();
            } catch (Exception e) {  
                throw new Exception(e.Message);  
            }  
        } 

        public IEnumerable<dynamic> ReadMeterReadingCsvFile(IFormFile file) {  
            try
            {
                using var reader = new StreamReader(file.OpenReadStream(), Encoding.Default);
                using var csv = new CsvReader(reader,CultureInfo.CurrentCulture);

                var records = csv.GetRecords<dynamic>();
                return records.ToList();
            } catch (Exception e) {  
                throw new Exception(e.Message);  
            }  
        } 
        

        //public void WriteCsvFile(string path, IEnumerable<Customer> customer)
        //{
        //    using var sw = new StreamWriter(path, false, new UTF8Encoding(true));
        //    using var cw = new CsvWriter(sw, CultureInfo.CurrentCulture);
        //    cw.WriteHeader <Customer> ();  
        //    cw.NextRecord();  
        //    foreach(var c in customer) {  
        //        cw.WriteRecord <Customer> (c);  
        //        cw.NextRecord();  
        //    }
        //}
    }
}
