
namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            CustomerBasket? basket = await _basketRepository.GetBasketAsync(BasketId);

            if (basket is null)
                return null!;

            decimal shippingPrice = 0m;

            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.DeliveryMethodsRepo.GetByIdAsync(basket.DeliveryMethodId.Value);
                basket.ShippingPrice = deliveryMethod!.Cost;
                shippingPrice = deliveryMethod.Cost;
            }


            if(basket.Items.Count > 0)
            {
                foreach (BasketItem item in basket.Items)
                {
                    Product? product = await _unitOfWork.ProductsRepo.GetByIdAsync(item.Id);
                    if (item.Price != product!.Price) 
                        item.Price = product.Price; 
                }
            }

            PaymentIntentService paymentIntentService = new();
            PaymentIntent paymentIntent;

            if(string.IsNullOrEmpty(basket.PaymentIntentId)) // Create new PaymentIntent
            {
                PaymentIntentCreateOptions createOptions = new()
                {
                    Amount = (long) basket.Items.Sum(i => i.Price * i.Quantity * 100)! + (long)shippingPrice,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };

                paymentIntent = await paymentIntentService.CreateAsync(createOptions);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update Existing Payment Intent
            {
                PaymentIntentUpdateOptions updateOptions = new()
                {
                    Amount = (long)basket.Items.Sum(i => i.Price * i.Quantity * 100)! + (long)shippingPrice
                };

                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }

            await _basketRepository.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<Order> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentID, bool isSucceeded)
        {
            OrderWithPaymentIntentSpecs spec = new(paymentIntentID);
            Order? order = await _unitOfWork.OrdersRepo.GetByIdWithSpecAsync(spec);

            if(isSucceeded) 
                order!.Status = OrderStatus.PaymentReceived;
            else
                order!.Status = OrderStatus.PaymentFailed;

            _unitOfWork.OrdersRepo.Update(order);
            await _unitOfWork.CompleteAsync();

            return order;
        }
    }
}
