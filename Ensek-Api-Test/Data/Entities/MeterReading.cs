using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ensek_Api_Test.Data.Entities
{
    public class MeterReading
    {
        public int Id { get; set; }
        [Required]
        public DateTime MeterReadingDateTime { get; set; }
        [Required]
        [MaxLength(5)]
        [MinLength(5)]
        public string MeterReadingValue { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
