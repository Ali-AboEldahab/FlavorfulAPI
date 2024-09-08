namespace Talabat.APIs.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            CustomerBasket? basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id)); 
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            CustomerBasket? mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            CustomerBasket? createdOrUpdated = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (createdOrUpdated is null)
                return BadRequest(new ApiResponse(400));
            return Ok(createdOrUpdated);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }

    }
}
