using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCase.Models
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Quantity { get; set; }
        public string PaidPrice { get; set; }
        public string UnitPrice { get; set; }
        public string RemoteCode { get; set; }
        public string Description { get; set; }
        public string vatPercentage { get; set; }
        public string discountAmount { get; set; }
    }
}
