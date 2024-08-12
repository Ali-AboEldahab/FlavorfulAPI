using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

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
