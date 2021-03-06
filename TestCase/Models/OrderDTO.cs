using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCase.Models
{
    public class OrderDTO
    {
        public string OrderNumber { get; set; }
        public List<ProductDTO> Products { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
