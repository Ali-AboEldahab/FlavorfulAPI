using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            Address address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            Order? order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);

            if (order is null)
                return BadRequest(new ApiResponse(400));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser(string email)
        {
            IReadOnlyList<Order>? orders = await _orderService.CreateOrderForUserAsync(email);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderForUser(int id,string email)
        {
            Order? order = await _orderService.CreateOrderByIdForUserAsync(id, email);  
            if (order is null)
                return NotFound(new ApiResponse(404));
            return Ok(order);
        }
    }
}
