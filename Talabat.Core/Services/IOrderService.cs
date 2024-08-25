namespace Talabat.Core.Services
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string? buyerEmail, string BasketId, int deliveryMethodId, Address shippingAddress);
        Task<IReadOnlyList<Order>> CreateOrderForUserAsync(string? buyerEmail);
        Task<Order?> CreateOrderByIdForUserAsync(int orderId, string? buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}
