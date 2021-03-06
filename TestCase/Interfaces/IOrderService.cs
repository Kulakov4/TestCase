using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Models;

namespace TestCase.Interfaces
{
    public interface IOrderService: ICrudService<Order>
    {
        public IEnumerable<Order> GetNotWorkedOrders();
    }
}
