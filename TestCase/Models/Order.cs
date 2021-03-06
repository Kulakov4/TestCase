using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestCase.Models
{
    public class Order: BaseEntity
    {
        public string System_type { get; set; }
        [Required]
        public ulong Order_number { get; set; }
        [Required]
        public string Source_order { get; set; }
        public string Converted_order { get; set; }
        [Required]
        public DateTime Created_at { get; set; }
    }
}
