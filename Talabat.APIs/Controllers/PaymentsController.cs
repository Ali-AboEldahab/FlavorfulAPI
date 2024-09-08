using Stripe;

namespace Talabat.APIs.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private const string _whSecret = "cb54da71fa8131f43b1c54efb31fc6b0302fe1d4b6e435c6ec32043b49fbd7c7";

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            CustomerBasket? basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket is null)
                return BadRequest(new ApiResponse(400, "An Error Occured whith your Basket"));
            return Ok(basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            
            Event? stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"],_whSecret);

            PaymentIntent? paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            Order order;

            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent!.Id,true);
                break;
                case Events.PaymentIntentPaymentFailed:
                    order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent!.Id,false);
                break; 
            }

            return new EmptyResult();
        }
    }
}
