namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod delivryMethod, ICollection<OrderItem> items, decimal? subtotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DelivryMethod = delivryMethod;
            Items = items;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
        public DeliveryMethod? DelivryMethod { get; set; } // Navigational prop
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal? Subtotal { get; set; }
        public decimal? GetTotal() => Subtotal + DelivryMethod!.Cost;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
