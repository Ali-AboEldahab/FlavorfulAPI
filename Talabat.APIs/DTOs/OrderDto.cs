namespace Talabat.APIs.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
