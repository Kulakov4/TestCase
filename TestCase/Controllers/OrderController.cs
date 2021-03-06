using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TestCase.Interfaces;
using TestCase.Models;

namespace TestCase.Controllers
{
    [ApiController]
    [Route("api/order/")]
    public class OrderController : ControllerBase
    {
        private ICrudService<Order> orderService;

        public OrderController(ICrudService<Order> orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        [Route("{system_type}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(SuccessResult<IEnumerable<PhoneBookRecord>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(string system_type, [FromBody] OrderDTO orderModel)
        {
            if (string.IsNullOrWhiteSpace(system_type) || orderModel == null)
                return BadRequest();
            try
            {
                string json_order = JsonSerializer.Serialize(orderModel);

                var new_order = new Order
                {
                    System_type = system_type,
                    Order_number = Convert.ToUInt64( orderModel.OrderNumber ),
                    Source_order = json_order,
                    Created_at = DateTime.Now
                };
                await orderService.Insert(new_order);
                await orderService.Save();
                return new EmptyResult();
            }
            catch
            {
                return StatusCode(500);
            }

        }
    }
}
