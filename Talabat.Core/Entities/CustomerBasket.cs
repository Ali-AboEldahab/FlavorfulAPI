namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
            //for 0 on cart
            Items = new List<BasketItem>();
        }
    }
}
