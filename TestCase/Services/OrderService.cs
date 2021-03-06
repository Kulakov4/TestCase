using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Data;
using TestCase.Interfaces;
using TestCase.Models;
using TestCase.Services;

namespace TestCase.Services
{
    public class OrderService : CrudService<Order>, IOrderService
    {
        public OrderService(ApplicationDbContext DbContext) : base(DbContext) {
        }

        public IEnumerable<Order> GetNotWorkedOrders()
        {
            var result = Where (order => string.IsNullOrWhiteSpace(order.Converted_order));
            return result;
        }
    }
}
