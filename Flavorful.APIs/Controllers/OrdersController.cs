namespace Flavorful.APIs.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            string? buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            Address address = _mapper.Map<AddressDto, Address>(orderDto.shipToAddress);
            Order? order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);

            if (order is null)
                return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            string? buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            IReadOnlyList<Order> orders = await _orderService.CreateOrderForUserAsync(buyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("GetOrder/{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
        {
            string? buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            Order? order = await _orderService.CreateOrderByIdForUserAsync(id, buyerEmail);  
            if (order is null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
            => Ok(await _orderService.GetDeliveryMethodAsync()); 
    }
}
