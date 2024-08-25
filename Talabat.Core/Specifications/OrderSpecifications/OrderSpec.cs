namespace Talabat.Core.Specifications.OrderSpecifications
{
    public class OrderSpec : BaseSpecifications<Order>
    {
        public OrderSpec(string buyerEmail)
            :base(o => o.BuyerEmail == buyerEmail)
        {
            Includes.Add( o => o.DelivryMethod!);
            Includes.Add(o => o.Items);
            AddOrderBy(o => o.OrderDate);
        }
        public OrderSpec(int OrderId , string buyerEmail)
            :base( o => o.Id == OrderId &&  o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DelivryMethod!);
            Includes.Add(o => o.Items);
        }
    }
}
