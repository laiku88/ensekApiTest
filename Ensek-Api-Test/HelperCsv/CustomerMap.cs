using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Ensek_Api_Test.Data.Entities;
using Ensek_Api_Test.DTOs;

namespace Ensek_Api_Test.HelperCsv
{
    public sealed class CustomerMap: ClassMap<Customer>
    {
        public CustomerMap()
        {
            Map(x => x.FirstName).Name("FirstName");
            Map(x => x.LastName).Name("LastName");
            Map(x => x.Id).Name("AccountId");
        }
    }
}
