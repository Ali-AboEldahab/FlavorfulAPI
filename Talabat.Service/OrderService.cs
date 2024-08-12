using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepository;
using Talabat.Core.Services;
using Talabat.Core.Specifications.OrderSpecifications;


namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string BasketId, int deliveryMethodId, Address shippingAddress)
        {
            CustomerBasket? basket = await _basketRepo.GetBasketAsync(BasketId);
            List<OrderItem> orderItems = [];
            if (basket?.Items.Count > 0)
            {
                IGenericRepository<Product> prodRepo = _unitOfWork.ProductsRepo;
                foreach (BasketItem item in basket.Items)
                {
                    Product? product = await prodRepo.GetByIdAsync(item.Id);
                    ProductItemOrdered? productItemOrdered = new(item.Id,product!.Name!,product.PictureUrl!);
                    OrderItem? orderItem = new(productItemOrdered, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }

            decimal? subtotal = orderItems.Sum(oi => oi.Price * oi.Quantity); 
            
            DeliveryMethod? deliverMethod = await _unitOfWork.DeliveryMethodsRepo.GetByIdAsync(deliveryMethodId);

            Order? order = new(buyerEmail, shippingAddress, deliverMethod!, orderItems, subtotal);

            await _unitOfWork.OrdersRepo.AddAsync(order);
            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return null;

            return order;
        }
         
        public async Task<IReadOnlyList<Order>> CreateOrderForUserAsync(string buyerEmail)
        {
            IGenericRepository<Order> ordersRepo = _unitOfWork.OrdersRepo;
            OrderSpec spec = new(buyerEmail);
            IReadOnlyList<Order>? orders = await ordersRepo.GetAllWithSpecAsync(spec);
            return orders;
        }

        public async Task<Order?> CreateOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            IGenericRepository<Order>? orderRepo = _unitOfWork.OrdersRepo;
            OrderSpec spec = new(orderId,buyerEmail);
            Order? order = await orderRepo.GetByIdWithSpecAsync(spec);
            return order;
        }
    }
}
