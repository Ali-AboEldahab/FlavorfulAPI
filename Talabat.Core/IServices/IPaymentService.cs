namespace Talabat.Core.IServices
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId);
        Task<Order> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentID, bool isSucceeded);
    }
}
